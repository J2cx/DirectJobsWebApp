using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp7.MemberPage
{
    public class Rappel
    {
        private int id_annonce = 0;
        //private string id_user = "";
        private DateTime dateRappel = new DateTime();
        private string noteRappel = "";
        private DateTime dateCree = new DateTime();

        #region Attributes
        public int Id_annonce
        {
            get { return Id_annonce; }
        }
        /*public string Id_user
        {
            get { return id_user; }
        }*/
        public DateTime DateRappel
        {
            get { return dateRappel; }
        }
        public string NoteRappel
        {
            get { return noteRappel; }
        }
        #endregion


        public Rappel(int idann, /*string iduser,*/ DateTime dateR, string note)
        {
            id_annonce = idann;
            //id_user = iduser;
            dateRappel = dateR;
            noteRappel = note;
        }

    }
}