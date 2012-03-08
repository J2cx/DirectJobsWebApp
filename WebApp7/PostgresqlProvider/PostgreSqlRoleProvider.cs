using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Collections.Generic;
using Npgsql;

/* CONVERTED TO PostgreSQL on 9-14-2006 by Lee@woodchop.com - http://blog.woodchop.com
 * BASED ON THE ORIGINAL MySQL IMPLEMENTATION BY Andri (see original comments below)
 */

/* 
-- Please, send me an email (andriniaina@gmail.com) if you have done some improvements or bug corrections to this file
      or leave your modifications on the comments page on sprinj.com
	  
*/

namespace WebApp7.PostgresqlProvider
{

    public sealed class PostgreSqlRoleProvider : RoleProvider
    {

        //
        // Global connection string, generic exception message, event log info.
        //

        private const string rolesTable = "provider_roles";
        private const string usersInRolesTable = "provider_usersinroles";
        private ConnectionStringSettings pConnectionStringSettings;
        private string connectionString;


        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //
        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "PostgreSqlRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample PostgreSql Role provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                pApplicationName = config["applicationName"];
            }

            //
            // Initialize NpgsqlConnection.
            //
            pConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null || pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = pConnectionStringSettings.ConnectionString;
        }



        //
        // System.Web.Security.RoleProvider properties.
        //
        private string pApplicationName;
        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        //
        // RoleProvider.AddUsersToRoles
        //
        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                if (username.IndexOf(',') > 0)
                {
                    throw new ArgumentException("User names cannot contain commas.");
                }

                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is already in role.");
                    }
                }
            }


            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + usersInRolesTable +
                        " (Username, Rolename, ApplicationName) " +
                        " Values(@Username, @Rolename, @ApplicationName)", conn);

                NpgsqlParameter userParm = cmd.Parameters.Add("@Username", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                NpgsqlParameter roleParm = cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                NpgsqlTransaction tran = null;
                conn.Open();

                try
                {
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;

                    foreach (string username in usernames)
                    {
                        foreach (string rolename in rolenames)
                        {
                            userParm.Value = username;
                            roleParm.Value = rolename;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
                catch (NpgsqlException e)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw e;
                }
                conn.Close();
            }
        }



        //
        // RoleProvider.CreateRole
        //

        public override void CreateRole(string rolename)
        {
            if (rolename.IndexOf(',') > 0)
            {
                throw new ArgumentException("Role names cannot contain commas.");
            }

            if (RoleExists(rolename))
            {
                throw new ProviderException("Role name already exists.");
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO " + rolesTable +
                        " (Rolename, ApplicationName) " +
                        " Values(@Rolename, @ApplicationName)", conn);

                cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        //
        // RoleProvider.DeleteRole
        //
        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
            {
                throw new ProviderException("Role does not exist.");
            }

            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + rolesTable +
                        " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;


                NpgsqlCommand cmd2 = new NpgsqlCommand("DELETE FROM " + usersInRolesTable +
                        " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                cmd2.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd2.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                NpgsqlTransaction tran = null;
                conn.Open();
                try
                {
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd2.Transaction = tran;

                    cmd2.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (NpgsqlException e)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw e;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }


        //
        // RoleProvider.GetAllRoles
        //

        public override string[] GetAllRoles()
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                List<string> roles = new List<string>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT Rolename FROM " + rolesTable +
                      " WHERE ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;


                conn.Open();
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
                conn.Close();
                return roles.ToArray();
            }

        }


        //
        // RoleProvider.GetRolesForUser
        //
        public override string[] GetRolesForUser(string username)
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                List<string> roles = new List<string>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT Rolename FROM " + usersInRolesTable +
                    " WHERE Username = @Username AND ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@Username", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = username;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                conn.Open();
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
                conn.Close();

                return roles.ToArray();
            }
        }


        //
        // RoleProvider.GetUsersInRole
        //
        public override string[] GetUsersInRole(string rolename)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                List<string> roles = new List<string>();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT Username FROM " + usersInRolesTable +
                          " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                conn.Open();
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0)); ;
                    }
                    reader.Close();
                }
                conn.Close();
                return roles.ToArray();
            }
        }


        //
        // RoleProvider.IsUserInRole
        //
        public override bool IsUserInRole(string username, string rolename)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM " + usersInRolesTable +
                        " WHERE Username = @Username AND Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@Username", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = username;
                cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                conn.Open();
                long numRecs = Convert.ToInt64(cmd.ExecuteScalar());
                conn.Close();

                return (numRecs > 0);
            }
        }


        //
        // RoleProvider.RemoveUsersFromRoles
        //
        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in rolenames)
                {
                    if (!IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is not in role.");
                    }
                }
            }


            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM " + usersInRolesTable +
                        " WHERE Username = @Username AND Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                NpgsqlParameter userParm = cmd.Parameters.Add("@Username", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                NpgsqlParameter roleParm = cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;

                NpgsqlTransaction tran = null;
                conn.Open();
                try
                {
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;

                    foreach (string username in usernames)
                    {
                        foreach (string rolename in rolenames)
                        {
                            userParm.Value = username;
                            roleParm.Value = rolename;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    conn.Close();
                }
                catch (NpgsqlException e)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }

                    throw e;
                }
            }
        }


        //
        // RoleProvider.RoleExists
        //

        public override bool RoleExists(string rolename)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM " + rolesTable +
                          " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);

                cmd.Parameters.Add("@Rolename", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = ApplicationName;


                conn.Open();
                long numRecs = Convert.ToInt64(cmd.ExecuteScalar());
                conn.Close();

                return (numRecs > 0);
            }
        }

        //
        // RoleProvider.FindUsersInRole
        //

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT Username FROM " + usersInRolesTable + "` " +
                          "WHERE Username LIKE @UsernameSearch AND Rolename = @Rolename AND ApplicationName = @ApplicationName", conn);
                cmd.Parameters.Add("@UsernameSearch", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = usernameToMatch;
                cmd.Parameters.Add("@RoleName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = rolename;
                cmd.Parameters.Add("@ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar, 255).Value = pApplicationName;

                List<string> users = new List<string>();

                conn.Open();
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
                conn.Close();

                return users.ToArray();
            }
        }
    }
}