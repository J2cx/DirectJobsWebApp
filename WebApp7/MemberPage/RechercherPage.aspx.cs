using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Threading;

namespace WebApp7.MemberPage
{
    public partial class RechercherPage : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {
        private DataTable dataTableFromPage = MemberPage1.DataTableAnnonces.Copy();
        private static DataTable dataTableRechercher = new DataTable();
        private static bool rechercherToPage = false;
        private string posteRechercher = "";
        private string regionRechercher = "";
        private string userID;


        private string strText = "";
        private string strMetier = "";
        private string strSecteur = "";


        public static DataTable DataTableRechercher
        {
            set { dataTableRechercher = value; }
            get { return dataTableRechercher; }
        }

        public static bool RechercherToPage
        {
            set { rechercherToPage = value; }
            get { return rechercherToPage; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                string[] itemsArray = listBoxDeptItems.Value.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
                strText="";
                
                for (int i = 0; i < itemsArray.Length; i++)
                {
                    if (itemsArray[i].Trim().Length > 0)
                    {
                        if (i == 0)
                            strText = " AND (localisation LIKE '%" + itemsArray[i].ToString().Substring(0, 2) + "%'" + " OR localisation LIKE '%" + itemsArray[i].ToString().Substring(2) + "%'";
                        else
                        {
                            strText += " OR localisation LIKE '%" + itemsArray[i].ToString().Substring(0, 2) + "%'" + " OR localisation LIKE '%" + itemsArray[i].ToString().Substring(2) + "%'";
                       }
                        if (i == itemsArray.Length - 1)
                            strText += ")";
                        // Save the item to your database...
                    }
                   
                }


                string[] itemsArrayMetier = listBoxMetierItems.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                strMetier = "";
             
                for (int i = 0; i < itemsArrayMetier.Length; i++)
                {
                    if (itemsArrayMetier[i].Trim().Length > 0)
                    {
                        if (i == 0)
                            strMetier = " AND (metier LIKE '%" + itemsArrayMetier[i].ToString() + "%'";
                        else
                        {
                            strMetier += " OR metier LIKE '%" + itemsArrayMetier[i].ToString() + "%'";
                            
                        }
                        if (i == itemsArrayMetier.Length - 1)
                            strMetier += ")";
                        // Save the item to your database...
                    }

                }


                string[] itemsArraySecteur = listBoxSecteurItems.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                strSecteur  = "";

                for (int i = 0; i < itemsArraySecteur .Length; i++)
                {
                    if (itemsArraySecteur [i].Trim().Length > 0)
                    {
                        if (i == 0)
                            strSecteur = " AND (secteur  LIKE '%" + itemsArraySecteur[i].ToString() + "%'";
                        else
                        {
                            strSecteur += " OR secteur  LIKE '%" + itemsArraySecteur[i].ToString() + "%'";
                            
                        }
                        if (i == itemsArraySecteur.Length - 1)
                            strSecteur += ")";
                        // Save the item to your database...
                    }

                }
              

                listBoxDeptItems.Value = string.Empty;
                listBoxMetierItems.Value = string.Empty;
                listBoxSecteurItems.Value = string.Empty;
            }
            else
            {
                listBoxDeptItems.Value = string.Empty;
                listBoxMetierItems.Value = string.Empty;
                listBoxSecteurItems.Value = string.Empty;
            }
        


            //Label3.Text = dataTableFromPage.Rows.Count.ToString();
            
            String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData2", "context");
            //String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            String callbackScript = "function CallTheServer2(arg, context) {" + cbReference + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallTheServer2", callbackScript, true);

            //MemberPages myMasterPage = Page.Master as MemberPages;
            //userID=myMasterPage.UserID;
            userID = Session["UserID"].ToString();
            //Response.Write("UserID :" + userID + "!!!!!!!!!!!!");

            //ListBoxDeptSource datasource:
            InitListBoxDept();
            InitListBoxMetier();
            InitListBoxSecteur();
            //kInitDropDownListDate();

        }

        public void InitListBoxDept()
        {
            string[] listBoxSource = { 
                                        "01 Ain",
                                        "02 Aisne",
                                        "03 Allier",
                                        "04 Alpes-de-Haute-Provence",
                                        "05 Hautes-Alpes",
                                        "06 Alpes-Maritimes",
                                        "07 Ardèche",
                                        "08 Ardennes",
                                        "09 Ariège",
                                        "10 Aube",
                                        "11 Aude",
                                        "12 Aveyron",
                                        "13 Bouches-du-Rhône",
                                        "14 Calvados",
                                        "15 Cantal",
                                        "16 Charente",
                                        "17 Charente-Maritime",
                                        "18 Cher",
                                        "19 Corrèze",
                                        "21 Côte d'Or",
                                        "22 Côtes d'Armor",
                                        "23 Creuse",
                                        "24 Dordogne",
                                        "25 Doubs",
                                        "26 Drôme",
                                        "27 Eure",
                                        "28 Eure-et-Loir",
                                        "29 Finistère",
                                        "30 Gard",
                                        "31 Haute-Garonne",
                                        "32 Gers",
                                        "33 Gironde",
                                        "34 Hérault",
                                        "35 Ille-et-Vilaine",
                                        "36 Indre",
                                        "37 Indre-et-Loire",
                                        "38 Isère",
                                        "39 Jura",
                                        "40 Landes",
                                        "41 Loir-et-Cher",
                                        "42 Loire",
                                        "43 Haute-Loire",
                                        "44 Loire-Atlantique",
                                        "45 Loiret",
                                        "46 Lot",
                                        "47 Lot-et-Garonne",
                                        "48 Lozère",
                                        "49 Maine-et-Loire",
                                        "50 Manche",
                                        "51 Marne",
                                        "52 Haute-Marne",
                                        "53 Mayenne",
                                        "54 Moselle",
                                        "55 Meuse",
                                        "56 Morbihan",
                                        "57 Meurthe-et-Moselle",
                                        "58 Nièvre",
                                        "59 Nord",
                                        "60 Oise",
                                        "61 Orne",
                                        "62 Pas-de-Calais",
                                        "63 Puy-de-Dôme",
                                        "64 Pyrénées-Atlantiques",
                                        "65 Hautes-Pyrénées",
                                        "66 Pyrénées Orientales",
                                        "67 Bas-Rhin",
                                        "68 Haut-Rhin",
                                        "69 Rhône",
                                        "70 Haute-Saône",
                                        "71 Saône-et-Loire",
                                        "72 Sarthe",
                                        "73 Savoie",
                                        "74 Haute-Savoie",
                                        "75 Paris",
                                        "76 Seine-Maritime",
                                        "77 Seine-et-Marne",
                                        "78 Yvelines",
                                        "79 Deux-Sèvres",
                                        "80 Somme",
                                        "81 Tarn",
                                        "82 Tarn-et-Garonne",
                                        "83 Var",
                                        "84 Vaucluse",
                                        "85 Vendée",
                                        "86 Vienne",
                                        "87 Haute-Vienne",
                                        "88 Vosges",
                                        "89 Yonne",
                                        "90 Territoire de Belfort",
                                        "91 Essonne",
                                        "92 Hauts-de-Seine",
                                        "93 Seine-Saint-Denis",
                                        "94 Val-de-Marne",
                                        "95 Val-d'Oise",
                                        "2A Corse-du-Sud (Ajaccio)",
                                        "2B Corse-Haute (Bastia)",
                                        "Non determinés",
                                     };
            ListBoxDeptSource.DataSource = listBoxSource;
            ListBoxDeptSource.DataBind();
            ListBoxDeptSource.Attributes.Add("ondblclick", "DbClickListBoxSource('" + ListBoxDeptSource.ClientID + "','" + ListBoxDeptResult.ClientID + "','"+listBoxDeptItems.ClientID+"')");
            ListBoxDeptResult.Attributes.Add("ondblclick", "DbClickListBoxResult('" + ListBoxDeptResult.ClientID + "')");
        }

        public void InitListBoxMetier()
        {

            string[] listBoxSource = { 
                                       "Architecture, création, spectacle",
                                        "Autres", 
                                        "BTP et Second œuvre",
                                        "Commercial / Vente",
                                        "Comptabilité et finances",
                                        "Editions et écritures",
                                        "Formation / Education",
                                        "Gestion de projet / programme",
                                        "Hotellerie, restauration, tourisme",
                                        "Informatique et technologies",
                                        "Ingénieries",
                                        "Installation, maintenances, réparations",
                                        "Juridique",
                                        "Logistique, approvisonnement, transport",
                                        "Marketing",
                                        "Productions et opérations",
                                        "Qualité, inspection",
                                        "Recherche et analyses",
                                        "Ressources humaines",
                                        "Santé",
                                        "Sécurité",
                                        "Services administratifs",
                                        "Services clientèle et aux particuliers",
                                        "Stratégie et management",
                                     };
            ListBoxMetierSource.DataSource = listBoxSource;
            ListBoxMetierSource.DataBind();
            ListBoxMetierSource.Attributes.Add("ondblclick", "DbClickListBoxSource('" + ListBoxMetierSource.ClientID + "','" + ListBoxMetierResult.ClientID + "','" + listBoxMetierItems.ClientID + "')");
            ListBoxMetierResult.Attributes.Add("ondblclick", "DbClickListBoxResult('" + ListBoxMetierResult.ClientID + "')");
        }

        public void InitListBoxSecteur()
        {

            string[] listBoxSource = { 
                                       "Type de secteur Non Determinés",
                                        "Aéronautique / Aérospatiale",
                                        "Agriculture / Sylviculture /pêche /chasse",
                                        "Agroalimentaire",
                                        "Architecture /Design & services associés",
                                        "Art / Culture Loisir",
                                        "Association /Bénévolat",
                                        "Assurance & Mutualité",
                                        "Audiovisuel / Media /Diffusion Audio Vidéo",
                                        "Audit / Comptabilité /Fiscalité",
                                        "Automobile - Vente, Maintenance & Réparations",
                                        "Autres",
                                        "Autres ervices aux entreprises",
                                        "banques / Organismes Financiers",
                                        "Biens de consommation courante / Cosmétiques",
                                        "BTP",
                                        "Cabinets et Services Juridiques",
                                        "Cabinets conseils en management et stratégies",
                                        "Chimie",
                                        "Commerce de gros / Import Export",
                                        "Editions et imprimeries",
                                        "Energie / Eau",
                                        "Enseignement / Formations",
                                        "Gestion des déchets / recyclage",
                                        "Grande distribution/ Commerce de détail",
                                        "Hôtellerie",
                                        "Immobilier",
                                        "Industrie Production",
                                        "Industrie Automobile - constructeur - équipementier",
                                        "Industrie électronique",
                                        "Industrie Pharmaceutique",
                                        "Industrie Textile, Cuir, Confection",
                                        "Informatique - Harware",
                                        "Informatqiue Services",
                                        "Informatique - Software",
                                        "Ingénierie et Services Associés",
                                        "Internet / e-commerce",
                                        "Location",
                                        "Marine / Aéronautique",
                                        "Marketing / Communication / Publicité / RP",
                                        "Métaux / Minéraux",
                                        "Parc d'attraction et Salles de Spectacles",
                                        "Recrutement / Interim / Bureaux de placements",
                                        "Restauration",
                                        "Santé",
                                        "Santé équipement et Appareils",
                                        "Secteur Public",
                                        "Sécurité surveillance",
                                        "Service aux particuliers",
                                        "Services financiers",
                                        "Sports équipements et infrastructure",
                                        "Télécommunication",
                                        "Tourisme, voyages, transports de personnes",
                                        "Transports de marchandises, entreposage, stockage",
                                     };
            ListBoxSecteurSource.DataSource = listBoxSource;
            ListBoxSecteurSource.DataBind();
            ListBoxSecteurSource.Attributes.Add("ondblclick", "DbClickListBoxSource('" + ListBoxSecteurSource.ClientID + "','" + ListBoxSecteurResult.ClientID + "','" + listBoxSecteurItems.ClientID + "')");
            ListBoxSecteurResult.Attributes.Add("ondblclick", "DbClickListBoxResult('" + ListBoxSecteurResult.ClientID + "')");
        }
        /*
        public void InitDropDownListDate()
        {
            string[] listBoxSource = { 
                                       "Aujourd'hui",
                                        "7 derniers jours", 
                                        "30 derniers jours",
                                        "Tous les jours",
                                     };

            DropDownListDate.DataSource = listBoxSource;
            DropDownListDate.DataBind();
        }
        */

        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            /*
            dataTableRechercher.Clear();
            dataTableRechercher = dataTableFromPage.Clone();

            foreach (DataRow dr in dataTableFromPage.Select("intitule LIKE '%Gestion%'"))
            {
                dataTableRechercher.ImportRow(dr);
            }
            rechercherToPage = true;
            */
            
            
            if (TextBoxPoste.Text != null)
            {
                posteRechercher = TextBoxPoste.Text;
            }
            else
            {
                posteRechercher = "";  
            }
            if (TextBoxRegion.Text != null)
            {
                regionRechercher = TextBoxRegion.Text;
            }
            else
            {
                regionRechercher = "";
            }

            
                        
            if (Request["text"] != null) 
            {
                string[] strs = Request["text"].ToString().Split(','); 
                for (int i = 0; i < strs.Length-1; i++) 
                {
                    //Response.Write("TextBox" + (i + 1) + " value:" + strs[i].ToString() + "<br/>");
                    strText += " AND localisation LIKE '%" + strs[i].ToString() + "%'";
                }
            }
            
            string strDate = "";
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    strDate = "";
                    //Response.Write(DropDownList1.Text);
                    break;
                case 1:
                    //string dateToday=DateTime.Now.Date.ToShortDateString();
                    strDate = " AND datecrea >= #" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "#";
                    //Response.Write(DropDownList1.Text);
                    break;
                case 2:
                    string date7=(DateTime.Now.Date-new TimeSpan(7,0,0,0)).ToString("MM/dd/yyyy");
                    strDate = " AND datecrea >= #" + date7 + "#";
                    //Response.Write(DropDownList1.Text);
                    break;
                case 3:
                    string date30 = (DateTime.Now.Date - new TimeSpan(30, 0, 0, 0)).ToString("MM/dd/yyyy");
                    strDate = " AND datecrea >= #" + date30 + "#";
                    //Response.Write(DropDownList1.Text);
                    break;
            }

            //listboxdeptresult
            /*
            string[] tmpdept=new string[2];
            if (Request.Form[ListBoxDeptResult.ClientID] != null)
            {
                if (ListBoxDeptResult.Items != null && ListBoxDeptResult.Items.Count > 0)
                {
                    foreach (ListItem lidept in ListBoxDeptResult.Items)
                    {
                        tmpdept[0] = lidept.Value.ToString().Substring(0, 2);
                        tmpdept[1] = lidept.Value.ToString().Substring(2);
                        strText += " OR location LIKE '%" + tmpdept[0] + "%'" + " OR location LIKE '%" + tmpdept[1] + "%'";

                    }
                }
            }
            */




            MemberPage1.StrFliter = "poste LIKE '%"+posteRechercher+"%' AND localisation LIKE '%"+regionRechercher+"%'"+strText+strDate+strMetier+strSecteur;
           //Response.Write(MemberPage1.StrFliter);
           //DataRow[] drstest = WebIni.DataTableAnnonces.Select("datecrea > #03/20/2011#");
             //Server.Transfer("~/MemberPage/MemberPage1.aspx", true);
            Response.Redirect("~/MemberPage/MemberPage1.aspx");
        }



        private string tooltip;
        public void RaiseCallbackEvent(String eventArgument)
        {
            
            tooltip = GetTooltip(eventArgument);
        }
        
        public string GetCallbackResult()
        {
            return tooltip;
        }

        protected string GetTooltip(string str1)
        {
            string[] strsInsert;
            strsInsert = str1.Split(new string[]{";"},StringSplitOptions.None);
            if (strsInsert.Length >= 5)
            {
                //Response.Write("<script language='javascript'>alert('产品添加成功！')</script >");

                //Response.Write("UserID :"+userID+"!!!!!!!!!!!!");
                SQL.Requet(@"insert into recherche (id_user, poste,localization,metier,secteur,title) values ('" 
                    + userID + "','" + strsInsert[0] + "','"+strsInsert[1]+"','"+strsInsert[2]+"','"+strsInsert[3]+"','"+strsInsert[4]+"')");
                return "Done!!";
            }
            //return the data from database, I simply add somes comments instead.
            //DataTable dtresult=SQL.GetTable(@"select * from annonces_r where poste='"+str1+"' and entreprise='"+str2+"'");

            //if(dtresult != null)
            //{

            //strreturn = str1 + " : " + SQL.Get(@"select descriptionshort from annonces_r where id=" + str1);
            // strreturn=dtresult.Rows[0]["id"]+" : "+dtresult.Rows[0]["descriptionshort"];
            //}
            return "dont insert into the data base";
        }



        //part of tooltip example

        



    }
}