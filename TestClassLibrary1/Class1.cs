using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Extensions.Asp;
using NUnit.Extensions.Asp.AspTester;

namespace TestClassLibrary1
{
  
    public class GuestBookTest : WebFormTestCase
    {
        [Test]
        public void TestNothing()
        {

        }
        [Test]
        public void TestNothing2()
        {
        }
         [Test]
        public void TestLayout()
        {
            PanelTester PanelBackground = new PanelTester("PanelBackground", CurrentWebForm);
            
            //TextBoxTester name = new TextBoxTester("name", CurrentWebForm);
            //TextBoxTester comments = new TextBoxTester("comments", CurrentWebForm);
            //ButtonTester save = new ButtonTester("save", CurrentWebForm);
            //DataGridTester book = new DataGridTester("book", CurrentWebForm);
            Browser.GetPage("http://localhost:2350/Default.aspx");

            AssertVisibility(PanelBackground, true);
            //AssertVisibility(comments, true);
            //AssertVisibility(save, true);
            //AssertVisibility(book, true); 
        } 
    }
}
