using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NUnit.Extensions.Asp; 
using NUnit.Extensions.Asp.AspTester; 
using System.Text;
using System.Web.Security;
 
namespace WebApp7.MemberPage
{
    public partial class MemberPage1 : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {
        private DataTable dataTableDisplay = new DataTable();
        public static DataTable DataTableAnnonces = new DataTable();
        private static string strFliter = "";
        private string userID = "";
        private string userName = "";

        public static string StrFliter
        {
            get { return strFliter; }
            set { strFliter = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonEnabledCheck();
            StringBuilder context1 = new StringBuilder();
            context1.Append("alert('sb')");

            String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            String callbackScript = "function CallTheServer(arg, context) {" + cbReference + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer", callbackScript, true);


            String cbReference3 = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData3", "context");
            String callbackScript3 = "function CallTheServer3(arg, context) {" + cbReference3 + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer3", callbackScript3, true);

            String cbReference4 = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData4", "context");
            String callbackScript4 = "function CallTheServer4(arg, context) {" + cbReference4 + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer4", callbackScript4, true);

            String cbReference5 = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData5", "context");
            String callbackScript5 = "function CallTheServer5(arg, context) {" + cbReference5 + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer5", callbackScript5, true);

            String cbReferenceSaveConserver = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerDataSaveConserver", "context");
            String callbackScriptSaveConserver = "function CallTheServerSaveConserver(arg, context) {" + cbReferenceSaveConserver + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServerSaveConserver", callbackScriptSaveConserver, true);

        }

        private string GetPostBackControl()
        {
            string Outupt = "";

            //get the __EVENTTARGET of the Control that cased a PostBack(except Buttons and ImageButtons)
            string targetCtrl = Page.Request.Params.Get("__EVENTTARGET");
            if (targetCtrl != null && targetCtrl != string.Empty)
            {
                Outupt = targetCtrl;
            }
            else
            {
                //if button is cased a postback
                foreach (string str in Request.Form)
                {
                    Control Ctrl = Page.FindControl(str);
                    if (Ctrl is Button)
                    {
                        Outupt = Ctrl.ID;
                        break;
                    }
                }
            }
            return Outupt;
        }

        protected void MultiView1_Init(object sender, EventArgs e)
        {
            MembershipUser myObject = Membership.GetUser();
            userID = myObject.ProviderUserKey.ToString();
            userName = myObject.ProviderName.ToString();
            //Response.Write("MasterPage!! ClientID :"+userID);
            Session["UserId"] = userID;
            Session["userName"] = userName;

            if (this.IsPostBack)
            {

                string targetCtrl = Page.Request.Params.Get("__EVENTTARGET");
                if (targetCtrl != null && targetCtrl != "")
                {
                    string valueCtrl = Page.Request.Params.Get("__EVENTARGUMENT");
                    //string idannonce = valueCtrl.Split(' ')[1];
                    if (valueCtrl != null && valueCtrl != "")
                    {
                        if (SQL.Get(@"select id from annonces_m where id_annonce=" + valueCtrl + " and id_user='" + Session["UserID"].ToString() + "'") != null)
                        {
                            SQL.Requet(@"update annonces_m set supprimer='1' where id_annonce=" + valueCtrl + " and id_user='" + Session["UserID"].ToString() + "'");
                        }
                        else
                        {
                            SQL.Requet(@"insert into annonces_m (id_annonce, id_user, supprimer) values (" + valueCtrl + ",'" + Session["UserID"].ToString() + "','1')");
                        }
                    }
                }

            }
            MultiView1_Original();
        }

        private void MultiView1_Original()
        {
            if (MultiView1.Views.Count > 1)
            {
                return;
            }

            //get top 100 annonces by date DESC
            DataTableAnnonces = SQL.GetTopAnnonces();
            //later, should select the annonces by username and the profile of the user
            //like the depts and the end date of the contrat


            /*
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            HttpCookie aCookie;

            if (Request.Cookies["userinfo"] == null)
            {
                Response.Cookies["userInfo"]["userName"] = Session["UserName"].ToString();
                Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
                Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(1);
            }
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                aCookie = Request.Cookies[i];
                output.Append("Cookie name = " + Server.HtmlEncode(aCookie.Name)
                    + "<br />");
                output.Append("Cookie value = " + Server.HtmlEncode(aCookie.Value)
                    + "<br /><br />");
            }
             */

            dataTableDisplay = DataTableAnnonces.Clone();

            DataTable dtSupprime = SQL.GetTable(@"select id_annonce from annonces_m where id_user='" + Session["UserID"].ToString() + "' and supprimer = '1'");
            bool supprime = false;
            foreach (DataRow dr in DataTableAnnonces.Select(strFliter))
            {
                supprime = false;
                foreach (DataRow drSupprime in dtSupprime.Rows)
                {
                    if (drSupprime["id_annonce"].ToString() == dr["id"].ToString())
                    {
                        supprime = true;
                    }
                }
                if (!supprime)
                {
                    dataTableDisplay.ImportRow(dr);
                }
            }
            int nbAnnonces = 0;
            int nbPage = 1;
            int nbInPage = 50;
            //Table1.Width = TableHeader.Width;

            bool lu = false;
            int nbline = 0;

            foreach (DataRow dr in dataTableDisplay.Rows)
            {
                nbline++;

                nbAnnonces++;
                TableRow tRow = new TableRow();
                tRow.Width = TableHeaderRow1.Width;
                
                tRow.Attributes.Add("style", "text-aligne:left");
                ((WebControl)tRow).CssClass += "trAnn";
                tRow.Attributes.Add("id","annonce"+dr["id"].ToString());
                //tRow.Attributes.Add("onclick", "ShowAnnonce('"+dr["id"].ToString()+"');");
                //tRow.Attributes.Add("onclick", "TRow_Click;");
                //tRow.Attributes.Add("tooltip","tooltip!!!!!!!!!!!!!");
 
                TableCell dateCell = new TableCell();
                dateCell.Text = ((DateTime)dr["date"]).Date.ToShortDateString();
                dateCell.Width = TableHeaderCellDate.Width;
                tRow.Cells.Add(dateCell);

                TableCell posteCell = new TableCell();
                posteCell.Text = dr["poste"].ToString();
                posteCell.Width = TableHeaderCellPoste.Width;
                posteCell.Attributes.Add("class", "over");
                posteCell.Attributes.Add("class", " foo");
                //posteCell.Attributes.Add("onmouseover", "Show1('" + dr["id"].ToString() + "');");
                //posteCell.Attributes.Add("onmouseout", "Hide1();");
                posteCell.Attributes.Add("onclick", "ShowAnnonce('" + dr["id"].ToString() + "', this.parentNode.id);");
                tRow.Cells.Add(posteCell);

                TableCell entrepriseCell = new TableCell();
                entrepriseCell.Text = dr["entreprise"].ToString();
                entrepriseCell.Width = TableHeaderCellEntreprise.Width;
                tRow.Cells.Add(entrepriseCell);

                TableCell locationCell = new TableCell();
                locationCell.Text = dr["localisation"].ToString();
                locationCell.Width = TableHeaderCellLocation.Width;
                tRow.Cells.Add(locationCell);

                TableCell InfCell = new TableCell();
                Image imgNote = new Image();
                Image imgConserver = new Image();
                Image imgSupprimer = new Image();
                Image imgRappeler = new Image();
                imgNote.ImageUrl = "../IMG/Used/Note1.png";
                imgConserver.ImageUrl = "../IMG/Used/ico_do_save.png";
                imgSupprimer.ImageUrl = "../IMG/Used/ico_do_delete.png";
                imgRappeler.ImageUrl = "../IMG/Used/sts_ar.png";

                imgNote.Attributes.Add("onclick", "ShowNote('" + dr["id"].ToString() + "');");
                imgConserver.Attributes.Add("onclick", "SaveConserver('" + dr["id"].ToString() + "');");
                imgSupprimer.Attributes.Add("onclick", "SupprimerAnnonce('" + dr["id"].ToString() + "');");
                imgRappeler.Attributes.Add("onclick", "multiPopup('"+ dr["id"].ToString() +"');");

                imgNote.Attributes.Add("class", "action");
                imgConserver.Attributes.Add("class", "action");
                imgSupprimer.Attributes.Add("class", "action");
                imgRappeler.Attributes.Add("class", "action");

                //Button btnSupprime = new Button();
                //btnSupprime.Controls.Add(imgSupprimer);
                //btnSupprime.Click += new EventHandler(this.btnSupprimeClick);
                
                InfCell.Controls.Add(imgNote);
                InfCell.Controls.Add(imgConserver);
                InfCell.Controls.Add(imgSupprimer);
                InfCell.Controls.Add(imgRappeler);
                //locationInf.Text = dr["location"].ToString();
                InfCell.Width = TableHeaderCellInf.Width;
                tRow.Cells.Add(InfCell);

                DataTable dtLu = SQL.GetTable(@"select id_annonce from annonces_m where id_user='" + Session["UserID"].ToString() + "' and lu = '1'");
                foreach (DataRow drLu in dtLu.Rows)
                {
                    if (drLu["id_annonce"].ToString() == dr["id"].ToString())
                    {
                        lu = true;
                        ((WebControl)tRow).CssClass += " lu";
                    }
                }
                

                if(nbline%2==0)
                {
                    ((WebControl)tRow).CssClass += " linedouble";
                }

                if (nbAnnonces <= nbInPage)
                {
                    Table1.Rows.Add(tRow);
                }
                else if (nbAnnonces == (nbInPage * nbPage + 1))
                {
                    nbPage++;
                    Table tableTmp = new Table();
                    tableTmp.Width = TableHeader.Width;
                    tableTmp.Rows.Add(tRow);
                    View viewTmp = new View();
                    viewTmp.Controls.Add(tableTmp);
                    MultiView1.Views.Add(viewTmp);
                }
                else
                {
                    ((MultiView1.Views[MultiView1.Views.Count - 1]).Controls[0] as Table).Rows.Add(tRow);
                }

                //MultiView1.ActiveViewIndex = 0;
            }
        }

        private void MultiView1_Rechercher()
        {
            int nbAnnonces = 0;
            int nbPage = 1;
            int nbInPage = 5;

            foreach (DataRow dr in RechercherPage.DataTableRechercher.Rows)
            {
                nbAnnonces++;
                TableRow tRow = new TableRow();
                tRow.Width = TableHeaderRow1.Width;
                tRow.Attributes.Add("style", "text-aligne:left");
                TableCell dateCell = new TableCell();
                dateCell.Text = dr[1].ToString();
                dateCell.Width = TableHeaderCellDate.Width;
                tRow.Cells.Add(dateCell);

                TableCell posteCell = new TableCell();
                posteCell.Text = dr[7].ToString();
                posteCell.Width = TableHeaderCellPoste.Width;
                tRow.Cells.Add(posteCell);

                TableCell entrepriseCell = new TableCell();
                entrepriseCell.Text = dr[13].ToString();
                entrepriseCell.Width = TableHeaderCellEntreprise.Width;
                tRow.Cells.Add(entrepriseCell);

                TableCell locationCell = new TableCell();
                locationCell.Text = dr[8].ToString();
                //locationCell.Width = TableHeaderCellLocation.Width;
                tRow.Cells.Add(locationCell);

                if (nbAnnonces <= nbInPage)
                {
                    Table1.Rows.Add(tRow);
                }
                else if (nbAnnonces == (nbInPage * nbPage + 1))
                {
                    nbPage++;
                    Table tableTmp = new Table();
                    tableTmp.Width = TableHeader.Width;
                    tableTmp.Rows.Add(tRow);
                    View viewTmp = new View();
                    viewTmp.Controls.Add(tableTmp);
                    MultiView1.Views.Add(viewTmp);
                }
                else
                {
                    ((MultiView1.Views[MultiView1.Views.Count - 1]).Controls[0] as Table).Rows.Add(tRow);
                }
            }
        }

        protected void ButtonSuivante_Click(object sender, EventArgs e)
        {
            if (MultiView1.Views.Count > MultiView1.ActiveViewIndex + 1)
            {
                MultiView1.ActiveViewIndex++;
            }
            ButtonEnabledCheck();
        }

        protected void ButtonPreview_Click(object sender, EventArgs e)
        {
            if (MultiView1.ActiveViewIndex >0)
            {
                MultiView1.ActiveViewIndex--;
            }
            ButtonEnabledCheck();

        }

        protected void ButtonEnabledCheck()
        {
            if (MultiView1.Views.Count >1 && MultiView1.Views.Count > MultiView1.ActiveViewIndex + 1)
            {
                ButtonSuivante.Enabled = true;
            }
            else
            {
                ButtonSuivante.Enabled = false;
            }
            if (MultiView1.ActiveViewIndex > 0)
            {
                ButtonPreview.Enabled = true;
            }
            else
            {
                ButtonPreview.Enabled = false;
            }
        }


        private string tooltip="";
        public void RaiseCallbackEvent(String eventArgument)
        {
            //string[] fullpath = eventArgument.Split(new string[]{":::"},StringSplitOptions.RemoveEmptyEntries);
            //if (fullpath.Length == 2)
            {
                //tooltip = GetTooltip(fullpath[0], fullpath[1]);
            }
            //else
            {
              //  tooltip = "fullpath length not 2";
            }
            string[] strPres = eventArgument.Split(new string[]{"!!!"},StringSplitOptions.None);
            if (strPres.Length == 2)
            {
                switch (strPres[0])
                {
                    case "A": tooltip = GetTooltip(strPres[1]); break;
                    case "B": RaiseCallbackEvent11(strPres[1]); break;
                    case "C": RaiseCallbackEvent14(strPres[1]); break;
                    case "D": RaiseCallbackEvent15(strPres[1]); break;
                    case "E": RaiseCallbackEventSaveConserver(strPres[1]); break;
                    default: break;
                }
                
            }
        }

        public string GetCallbackResult()
        {
            return tooltip;
        }

        protected string GetTooltip(string str1)
        {
            //return the data from database, I simply add somes comments instead.
            //DataTable dtresult=SQL.GetTable(@"select * from annonces_r where poste='"+str1+"' and entreprise='"+str2+"'");
            string strreturn="";
            //if(dtresult != null)
            //{

            strreturn = str1+" : "+SQL.Get(@"select descriptionshort from annonces_r where id="+str1);
               // strreturn=dtresult.Rows[0]["id"]+" : "+dtresult.Rows[0]["descriptionshort"];
            //}
                return "this is just a demo: " +strreturn;
        }

        protected void TRow_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>alert('产品添加成功！')</script >");
        }

        public string StrDoc
        {
            get { return tooltip; }
        }
        public void RaiseCallbackEvent11(String eventArgument)
        {
            tooltip = "";
            if (SQL.Get(@"select id from annonces_m where id_annonce=" + eventArgument + " and id_user='" + Session["UserID"].ToString() + "'") != null)
            {
                SQL.Requet(@"update annonces_m set lu='1' where id_annonce=" + eventArgument + " and id_user='" + Session["UserID"].ToString() + "'");
            }
            else
            {
                SQL.Requet(@"insert into annonces_m (id_annonce, id_user, lu) values (" + eventArgument + ",'" + Session["UserID"].ToString() + "','1')");
            }


            string typeAnn = SQL.Get(@"select type_annonce from annonces_r where id=" + eventArgument);
            switch (typeAnn)
            {
                case "Fullpage":
                    tooltip = "Annonce " + eventArgument;// + " : " + SQL.Get(@"select description from annonces_r where id=" + eventArgument);
                    //Server.Transfer("~/MemberPage/AnnoncePage.aspx", true);
                    //Response.Write("<script language='javascript'>window.open(\"http://www.asp.net\"')</script >");
                    break;
                case "Lien":
                    tooltip = SQL.Get(@"select link from annonces_r where id=" + eventArgument);
                    
                    break;
                default: break;
            }
        }
        public void RaiseCallbackEvent14(String eventArgument)
        {
            tooltip = eventArgument+";;;";
            string note = SQL.Get(@"select note from annonces_m where id_annonce=" + eventArgument+" and id_user='"+Session["UserID"].ToString()+"'");
            if (note != null)
            {
                tooltip += note;
            }
        }
        public void RaiseCallbackEvent15(String eventArgument)
        {
            tooltip = "Fail";
            string[] splits=eventArgument.Split(new string[]{";;;"},StringSplitOptions.None);
            if(splits.Length==2)
            {
                if (SQL.Get(@"select id from annonces_m where id_annonce=" + splits[0] + " and id_user='" + Session["UserID"].ToString() + "'") != null)
                {
                    SQL.Requet(@"update annonces_m set note='" + splits[1] + "' where id_annonce=" + splits[0] + " and id_user='" + Session["UserID"].ToString() + "'");
                }
                else
                {
                    SQL.Requet(@"insert into annonces_m (id_annonce, id_user, note) values (" + splits[0] + ",'" + Session["UserID"].ToString() + "','" + splits[1] + "')");
                }
            }
            tooltip = "Suceed";
        }
        public void RaiseCallbackEventSaveConserver(String eventArgument)
        {
            tooltip = "Fail";
            if (SQL.Get(@"select id from annonces_m where id_annonce=" + eventArgument + " and id_user='" + Session["UserID"].ToString() + "'") != null)
            {
                SQL.Requet(@"update annonces_m set conserver='1' where id_annonce=" + eventArgument + " and id_user='" + Session["UserID"].ToString() + "'");
            }
            else
            {
                SQL.Requet(@"insert into annonces_m (id_annonce, id_user, conserver) values (" + eventArgument + ",'" + Session["UserID"].ToString() + "','1')");
            }
            tooltip = "Conserver Suceed";
        }

        protected void btnSupprimeClick(object sender, EventArgs e)
        {
            ((WebControl)sender).Parent.Parent.ID.ToString();
                /*
            if (SQL.Get(@"select id from annonces_m where id_annonce=" + (TableRow)((WebControl)sender).Parent.Parent).id + " and id_user='" + Session["UserID"].ToString() + "'") != null)
            {
                SQL.Requet(@"update annonces_m set conserver='1' where id_annonce=" + eventArgument + " and id_user='" + Session["UserID"].ToString() + "'");
            }
            else
            {
                SQL.Requet(@"insert into annonces_m (id_annonce, id_user, conserver) values (" + eventArgument + ",'" + Session["UserID"].ToString() + "','1')");
            }*/
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            if (TextBoxDateRappeler.Text != null)
            {
                DateTime MyDateTime;
                string idannonce = TextBoxIDAnnonceRappeler.Text;
                if (idannonce != null && idannonce!="")
                {
                    MyDateTime = new DateTime();
                    MyDateTime = DateTime.Parse(TextBoxDateRappeler.Text);
                    //string dateTimestamp = MyDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    if (SQL.Get(@"select id from annonces_m where id_annonce=" + idannonce + " and id_user='" + Session["UserID"].ToString() + "'") != null)
                    {
                        SQL.Requet(@"update annonces_m set rappeler='1' where id_annonce=" + idannonce + " and id_user='" + Session["UserID"].ToString() + "'");
                    }
                    else
                    {
                        SQL.Requet(@"insert into annonces_m (id_annonce, id_user, rappeler) values (" + idannonce + ",'" + Session["UserID"].ToString() + "','1')");
                    }
                    if (SQL.Get(@"select id from rappeler where id_annonce=" + idannonce + " and id_user='" + Session["UserID"].ToString() + "'") != null)
                    {
                        SQL.Requet(@"update rappeler set daterappler='" + MyDateTime + "' where id_user='" + Session["UserID"].ToString() + "' and id_annonce='" + idannonce + "'");
                    }
                    else
                    {
                        SQL.Requet(@"insert into rappeler  (id_annonce, id_user, daterappeler) values (" + idannonce + ",'" + Session["UserID"].ToString() + "','" + MyDateTime + "')");
                    }
                }
            }
        }
        //part of tooltip example
        /*
        protected void Page_Load(object sender, EventArgs e)
        {
            String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            String callbackScript = "function CallTheServer() {" + cbReference + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer", callbackScript, true);
        }
        */
    }


          
 




}