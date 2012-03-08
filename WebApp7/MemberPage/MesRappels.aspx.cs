using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApp7.MemberPage
{
    public partial class MesRappels : System.Web.UI.Page
    {
        private List<Annonce> AnnsRappel = new List<Annonce>();
        private List<Rappel> Rappels = new List<Rappel>();
        private string innerHtmlForLoad = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtRappels = new DataTable();
            dtRappels = SQL.GetTable("select * from rappeler where id_user='" + Session["UserID"].ToString() + "'");
            foreach (DataRow drr in dtRappels.Rows)
            {
                Rappels.Add(new Rappel(Convert.ToInt32(drr["id_annonce"]), (DateTime)drr["daterappeler"], drr["noterappeler"].ToString()));
                innerHtmlForLoad += LoadRappel(drr);
            }
            DivMesRappels.InnerHtml = innerHtmlForLoad;
        }


        protected string LoadRappel(DataRow drRappel)
        {
            
            string tmpInnerHtml = "";
            DataTable dtann = SQL.GetTable("select * from annonces_r where id=" + drRappel["id_annonce"]);
            if (dtann != null && dtann.Rows.Count >= 0)
            {
                DataRow dr=dtann.Rows[0];
                Annonce ann = new Annonce(dr["poste"].ToString(), dr["entreprise"].ToString(), dr["localisation"].ToString(), (DateTime)dr["date"], dr["description"].ToString());
                
                tmpInnerHtml = "<div> Mon Rappel " + drRappel["id"] + "<br /><div class='divposte'>" + drRappel["id_annonce"] + "</div><div>"+dr["poste"].ToString()+"</div></div><br />";
            }
                return tmpInnerHtml;    

            //document.getElementById('<%= myDiv.ClientID %>'); 
        }
    }



}
