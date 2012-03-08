using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using NUnit.Extensions.Asp;
using NUnit.Extensions.Asp.AspTester; 
using System.Diagnostics;

namespace WebApp7
{
    public partial class _Default : System.Web.UI.Page
    {
        public int Price { get; set; }
        private int _Price;

        protected void Page_Load(object sender, EventArgs e)
        {
            _Price = 5;

            String strh = new String('s',3);
            
            Console.WriteLine("Hello World.");

            Trace.Warn(strh);
            Debug.Write(Price.ToString());
        }

       
    }

    public class Hello
    {

        public int Id
        {
            get;
            set;
        }

        private int _Name;

        private int Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name=value;
            }
        }




    }
    public class HelpAttribute : Attribute
    {
        public HelpAttribute(String Descrition_in)
        {
            this.description = Descrition_in;
        }
        protected String description;
        public String Description
        {
            get
            {
                return this.description;

            }
        }
    }
    [Help("this is a do-nothing class")]
    public class AnyClass
    {
    }


}