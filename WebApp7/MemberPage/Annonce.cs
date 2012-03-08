using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp7.MemberPage
{
    public class Annonce:Dictionary<string,object>
    {
        //variable obligatoir
        private string poste = "";
        private string entreprise = "";
        private string localisation = "";
        private DateTime date = new DateTime();
        private string description = "";

        //variable peut etre null
        private string metier = "";
        private string secteur = "";

        #region Attributes
        public string Poste
        {
            get
            {
                return poste;
            }
            set
            {
                poste = value;
                this["Poste"] = value;
            }
        }
        public string Entreprise
        {
            get { return entreprise; }
            set { entreprise = value;
            this["Entreprise"] = value;
            }
        }
        public string Localisation
        {
            get { return localisation; }
            set { localisation = value;
            this["Localisation"] = value;
            }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value;
            this["Date"] = value;
            }
        }
        public string Description
        {
            get { return description; }
            set { description = value;
            this["Description"] = value;
            }
        }

        #endregion
        
        
        public Annonce()
        {
            this.Add("Poste","");
            this.Add("Entreprise","");
            this.Add("Localisation","");
            this.Add("Date",new DateTime());
            this.Add("Description", "");
        }

        public Annonce(string poste, string entreprise, string localisation, DateTime date, string description):this()
        {
            this["Poste"] = poste;
            this["Entreprise"] = entreprise;
            this["Localisation"] = localisation;
            this["Date"] = date;
            this["Description"] = description;
        }
    }
}