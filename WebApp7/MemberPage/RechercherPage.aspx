<%@ Page Language="C#" MasterPageFile="~/MemberPage/MemberPages.Master" AutoEventWireup="true" CodeBehind="RechercherPage.aspx.cs" Inherits="WebApp7.MemberPage.RechercherPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" onload="Page_Load_Content()">
    <script type="text/javascript" language="JavaScript">
    <!--
        function doClick() 
        {
            alert("You clicked me!");
        }
    //-->

        function add(thistext) {
            //if(thistext.isPrototypeOf("input"))
            if (thistext.value != null && thistext.value != "") {
                thistext.setAttribute("onblur", "detectnull(this)");
                thistext.setAttribute("onkeyup", "");
                var type = "Text";
                //Create an input type dynamically.
                var element = document.createElement("input");
                //Assign different attributes to the element.     
                element.setAttribute("type", type);
                //element.setAttribute("value", type);    
                element.setAttribute("name", type);
                element.setAttribute("onKeyUp", "add(this)");
                var foo = document.getElementById("fooBar");
                //Append the element in page (in span).
                var brfoo = document.createElement("br");

                foo.appendChild(element);
                foo.appendChild(brfoo);

            }
        }
        function detectnull(thistext) {
            //if(thistext.isPrototypeOf("input"))
            if (thistext.value == "") {

                var foo = document.getElementById("fooBar");
                //Append the element in page (in span).
                var brfoo = thistext;
                do {
                    brfoo = brfoo.nextSibling;
                } while (brfoo && brfoo.nodeType != 1);

                alert(brfoo.outerHTML );
                foo.removeChild(brfoo);
                foo.removeChild(thistext);
                //thistext.setAttribute("style", "display : none");
            }
        }
        function Button2_onclick() {
            var strRecherch;
            var listBoxDept,listBoxMetier,listBoxSecteur;
            strRecherch = document.getElementById('<%=TextBoxPoste.ClientID%>').value;
            listBoxDept = document.getElementById('<%= ListBoxDeptResult.ClientID%>');
            listBoxMetier = document.getElementById('<%= ListBoxMetierResult.ClientID%>');
            listBoxSecteur = document.getElementById('<%= ListBoxSecteurResult.ClientID%>');
            strRecherch += ";";
            if (listBoxDept != null) {
                var i;
                for (i = 0; i < listBoxDept.options.length; i++) {
                    strRecherch += listBoxDept.options[i].value +",";
                }
            }
            strRecherch += ";";
            if (listBoxMetier != null) {
                var i;
                for (i = 0; i < listBoxMetier.options.length; i++) {
                    strRecherch += listBoxMetier.options[i].value + ",";
                }
            }
            strRecherch += ";";
            if (listBoxSecteur != null) {
                var i;
                for (i = 0; i < listBoxSecteur.options.length; i++) {
                    strRecherch += listBoxSecteur.options[i].value + ",";
                }
            }
            strRecherch += ";";
            var name = prompt("Please enter a name for the search", "Recherche");
            if (name != null) {
                strRecherch += name;
            }

            CallTheServer2(strRecherch, '');
        }


        function ReceiveServerData2(arg, context) {
            alert(arg);
        }

        function DbClickListBoxResult(IdResult) {
            var lbresult = document.getElementById(IdResult);
            if (lbresult != null) {
                lbresult.remove(lbresult.options.selectedIndex);
            }
        }



        function DbClickListBoxSource(IdSource,IdResult,Idhdn) {
            //alert("Db click!! 有木有啊！！");
            var lbresult = document.getElementById(IdResult);
           /* if (lbresult != null) {
                alert("get element listbox 有木有啊！！！！！");
            }
            else {
                alert("又NULL了。。。 !!! 有木有啊！！！！！");
            }
            */
            var lbsource = document.getElementById(IdSource);
            //myOption = document.createElement("Option");
            if ((lbresult != null) && (lbsource != null)) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = lbsource.options[lbsource.options.selectedIndex].text;
                newOption.value = lbsource.options[lbsource.options.selectedIndex].value;
                var i;
                for (i = 0; i < lbresult.options.length; i++) {
                    if (lbresult.options[i].text == newOption.text)
                        break;
                }
                if (i == lbresult.length) {
                    lbresult.options[i] = newOption;
                }
            }
            var itemList = document.getElementById(Idhdn);
            var elements = "";
            var intCount = lbresult.options.length;

            //store the elements in a hidden input that we can get server side 
            for (i = 0; i < intCount; i++) {
                elements += lbresult.options(i).text + ',';
                //elements += listBox.options(i).value + ';';
            }

            itemList.value = elements; 


            /*if (itemList.length > 0)
                itemList += ',';
                itemList += newOption.value;
            */
            return true;
 
        }

    </script>

    <style type="text/css">
        .listbox
        {
            Height:240px;
            Width:156px;
            
        }
    </style>

    <h2>Recherche :</h2>
    
    <div id="divDept">
    <p id ="haha"+"lala">Localisation :</p>
        <asp:ListBox ID="ListBoxDeptSource" class="listbox" 
            runat="server" AutoPostBack="false"   >
        </asp:ListBox>
        <img src="../IMG/arrow1.jpg" alt="" style="height: 40px; width: 40px"/>
        <asp:ListBox ID="ListBoxDeptResult" class="listbox" 
            runat="server" 
            AutoPostBack="false" ></asp:ListBox>
        <input type="hidden" id="listBoxDeptItems" name="listBoxDeptItems" runat="server" />
    </div>

    
    <div id="divMetier">
    <p>Métier :</p>
        <asp:ListBox ID="ListBoxMetierSource" class="listbox" 
            runat="server" AutoPostBack="false"   >
        </asp:ListBox>
        <img src="../IMG/arrow1.jpg" alt="" style="height: 40px; width: 40px"/>
        <asp:ListBox ID="ListBoxMetierResult" class="listbox" 
            runat="server" 
            AutoPostBack="false" ></asp:ListBox>
        <input type="hidden" id="listBoxMetierItems" name="listBoxMetierItems" runat="server" />
    </div>

    <div id="divSecteur">
    <p>Secteur :</p>
        <asp:ListBox ID="ListBoxSecteurSource" class="listbox" 
            runat="server" AutoPostBack="false"   >
        </asp:ListBox>
        <img src="../IMG/arrow1.jpg" alt="" style="height: 40px; width: 40px"/>
        <asp:ListBox ID="ListBoxSecteurResult" class="listbox" 
            runat="server" 
            AutoPostBack="false" ></asp:ListBox>
        <input type="hidden" id="listBoxSecteurItems" name="listBoxMetierItems" runat="server" />
    </div>

    <div id="divDate">
    <p>Date :</p>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>tous les jours</asp:ListItem>
        <asp:ListItem>aujourd&#39;hui</asp:ListItem>
        <asp:ListItem>7 derniers jours</asp:ListItem>
        <asp:ListItem>30 derniers jours</asp:ListItem>
    </asp:DropDownList>
    

    

    </div>

    <asp:Label ID="LabelPoste" runat="server" Text="Mot clé:"></asp:Label>
    <asp:TextBox ID="TextBoxPoste" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="ButtonTest" runat="server" Text="Chercher" 
        onclick="ButtonTest_Click" />
    <input id="Button2" type="button" value="Enregister la Recheche" 
        onclick="return Button2_onclick()" />
    <br />
    
    
    <!--
    
    <asp:Label ID="LabelRegion" runat="server" Text="Region :"></asp:Label>
    <asp:TextBox ID="TextBoxRegion" runat="server"></asp:TextBox>
    <br />
    <br />
    
    <br />
    <span id="fooBar">
    <asp:Label ID="Label4" runat="server" Text="Chercher par dept:"></asp:Label>
    <br />
    <input id="Text1" type="text" name="text" onkeyup="add(this)"/>

    <br />
   
    </span>
        
-->
    

</asp:Content>

