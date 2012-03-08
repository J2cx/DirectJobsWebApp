using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace WebApp7.MemberPage
{
    public partial class AnnoncePage : System.Web.UI.Page
    {
        private string strDoc = "";
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                strDoc = Request.QueryString["id"];
                titleAnnocne.InnerText = strDoc;
                int nums = GetNumberFromStrFaster(strDoc);
                tdPoste.InnerText = SQL.Get(@"select poste from annonces_r where id=" + nums);
                DateTime dt =(DateTime)SQL.GetObject(@"select Date from annonces_r where id=" + nums);
                tdDate.InnerText = dt.Date.ToShortDateString();
                tdLocalisation.InnerText = SQL.Get(@"select localisation from annonces_r where id=" + nums);
                tdContrat.InnerText = SQL.Get(@"select contract from annonces_r where id=" + nums);
                pDescription.InnerText= SQL.Get(@"select description from annonces_r where id=" + nums);
            }


            /*&²
            if (PreviousPage is MemberPage1)
            {
                strDoc = (PreviousPage as MemberPage1).StrDoc;
                string[] strsDoc=strDoc.Split(new string[]{" : "},StringSplitOptions.RemoveEmptyEntries);
                if (strsDoc.Length == 2)
                {
                    titleAnnocne.InnerText = strsDoc[0];

                }
            }
             */
        }
        public static int GetNumberFromStrFaster(string str)
        {
            int number = Convert.ToInt32(str.Split(' ')[1]); 
            return number;
        }
    }
}