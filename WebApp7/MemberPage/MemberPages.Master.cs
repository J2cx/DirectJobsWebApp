using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApp7.MemberPage
{
    public partial class MemberPages : System.Web.UI.MasterPage
    {
        private string userID = "";
        private string userName = "";

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                MembershipUser myObject = Membership.GetUser();
                userID = myObject.ProviderUserKey.ToString();
                userName = myObject.ProviderName.ToString();
                // Response.Write("MasterPage!! ClientID :"+UserID);
                Session["UserId"] = userID;
                Session["UserName"] = userName;
            }
            else 
            {
                userID =Session["UserId"].ToString() ;
                userName = Session["UserName"].ToString();
            }
        }

        protected void LinkButtonAnnonces_Click(object sender, EventArgs e)
        {
            MemberPage1.StrFliter = "";
            Response.Redirect("~/MemberPage/MemberPage1.aspx");
        }
    }
}