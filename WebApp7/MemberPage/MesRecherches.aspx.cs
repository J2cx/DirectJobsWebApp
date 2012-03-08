using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApp7.MemberPage
{
    public partial class MesRecherches : System.Web.UI.Page
    {
        private string userID = "";
        private DataTable dtMesRecherches=new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //MemberPages myMasterPage = Page.Master as MemberPages;
            //userID = myMasterPage.UserID;
            userID = Session["UserID"].ToString();

            dtMesRecherches = SQL.GetTable(@"select * from recherche where id_user='"+userID+"'");
            if (dtMesRecherches.Rows.Count <= 0)
            {
                Response.Write("<script language='javascript'>alert('Il ny a pas de recherche..')</script >");
                //Response.Write("dt rows <=0....");
            }
            else
            {
                LoadRecherches(dtMesRecherches);
                //Response.Write("Good Job..");
            }

        }

        protected void LoadRecherches(DataTable dtShow)
        {
            string innerHtmlForLoad = "";
            string tmpInnerHtml = "";
            foreach(DataRow drRecherche in dtShow.Rows)
            {
                tmpInnerHtml = "<div> Mes Recherche "+drRecherche["id"]+"<br /><div class='divposte'>"+drRecherche["poste"]+"</div></div><br />";
                innerHtmlForLoad += tmpInnerHtml;
            }
            DivMesRecherches.InnerHtml = innerHtmlForLoad;
 
            //document.getElementById('<%= myDiv.ClientID %>'); 
        }

    }
}