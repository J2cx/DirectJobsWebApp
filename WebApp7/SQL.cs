using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using System.Web.Configuration;

//using WebApp7.PostgresqlProvider;

namespace WebApp7
{
    public class SQL
    {
        private static string connectionString="";

        public string ConnectionString
        {
            set { connectionString = value; }
            get { return connectionString; }
        }

        public static void Initialize()
        {
            //connectionString = "DATABASE=db_jobs;SERVER=localhost;PORT=5432;UID=job_user;Password=482job4;Encoding=unicode";
            connectionString = WebConfigurationManager.ConnectionStrings["postgresqlConnString"].ConnectionString;

        }

        public static DataTable GetTopAnnonces()
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                DataTable dataTable = null;
                DataSet ds = new DataSet();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM annonces_r order by date DESC LIMIT 100", conn);


                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                dataTable = ds.Tables[0];

                return dataTable;
            }
        }


        public static DataTable GetAllAnnonces()
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                DataTable dataTable=null;
                DataSet ds = new DataSet();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM annonces_r", conn);


                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                dataTable = ds.Tables[0];
                
                return dataTable;
            }
        }

        public static string Get(string strcmd)
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {


                string ret = null;
                NpgsqlCommand cmd = new NpgsqlCommand(strcmd, conn);
                conn.Open();
                try
                {
                    ret = cmd.ExecuteScalar().ToString();
                }
                catch(Exception e)
                {
                    
                }
                finally
                {
                    conn.Close();
                }
                return ret;
            }
        }

        public static object GetObject(string strcmd)
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {


                object ret = new DateTime();
                NpgsqlCommand cmd = new NpgsqlCommand(strcmd, conn);
                conn.Open();
                try
                {
                    ret = cmd.ExecuteScalar();
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                }
                return ret;
            }
        }

        public static DataTable GetTable(string command)
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                DataTable dataTable = null;
                DataSet ds = new DataSet();
                NpgsqlCommand cmd = new NpgsqlCommand(command, conn);


                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                dataTable = ds.Tables[0];

                return dataTable;
            }
        }

        public static void Requet(string command)
        {
            if (connectionString == null || connectionString.Length <= 0)
            {
                Initialize();
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                //DataTable dataTable = null;
                DataSet ds = new DataSet();
                NpgsqlCommand cmd = new NpgsqlCommand(command, conn);


                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                //dataTable = ds.Tables[0];

                //return dataTable;
            }
        }
    }
}