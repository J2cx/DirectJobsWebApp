<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MemberPage/MemberPages.Master" CodeBehind="MemberPage1.aspx.cs" Inherits="WebApp7.MemberPage.MemberPage1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Implements Interface="System.Web.UI.ICallbackEventHandler" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Show1(id_annonce) {        //document.getElementById("Popup_tooltip").style.overflow="auto";
            
        CallTheServer("A!!!"+id_annonce, '');

        var popup = document.getElementById("Popup_tooltip");
        //popup.style.display = "block";
        x = event.clientX + document.body.scrollLeft;
        y = event.clientY + document.body.scrollTop + 10;

        popup.style.left = x + "px";
        popup.style.top = y + "px";
        //if (popup.style.left !=x) {
        //  alert("ni tmd qu si!!!!!!!!!!");

        //}
        popup.style.display = "block";
        
    }
    function Hide1() {        /* hide the pop-up */
        //this.style.cursor = 'hand';
        document.getElementById("Popup_tooltip").style.display = "none";
    }

    function ReceiveServerData(arg, context) {
        document.getElementById('Popup_tooltip').innerHTML = arg;
        //document.getElementById('Popup_tooltip').style.left = positionX;
        //document.getElementById('Popup_tooltip').style.top = positionY;
        document.getElementById('Popup_tooltip').style.display = 'block';

    }


    function ProcessCallBackError(arg, context) {
        alert("An error has occurred.");
    }


    function Button1_onclick() {
        var popup = document.getElementById("Popup_tooltip");
        alert(popup.offsetLeft + "px");
        popup.style.left = popup.offsetLeft - 10 + "px";
    }

    function ShowAnnonce(strDoc, thisTR) {
        var trlu = document.getElementById(thisTR);
        trlu.className+=" lu";
        CallTheServer3("B!!!" + strDoc, '');
    }

    function ReceiveServerData3(arg, context) {
        if (arg.indexOf("Annonce") == 0) {
            window.open("AnnoncePage.aspx?id=" + arg);
        }
        else {
            window.open(arg);
        }
    }


    function ShowNote(idannonce) {
        CallTheServer4("C!!!" + idannonce, '');
    }

    function ReceiveServerData4(arg, context) {
        var mySplitResult = arg.split(";;;");
        if (mySplitResult.length == 2) {
            jPrompt('Please enter your note for annonce' + mySplitResult[0], mySplitResult[1], 'Prompt Dialog', function (r) {
                if (r) {
                    alert('You entered ' + r);
                    CallTheServer5("D!!!" + mySplitResult[0] + ";;;" +r, '');
                }
            });
            
            //var note= prompt("Please enter your note for annonce " + mySplitResult[0], mySplitResult[1]);
            //if (note != null) {
            //    CallTheServer5("D!!!" + mySplitResult[0] + ";;;" + note, '');
            
        }
    }
    function ReceiveServerData5(arg, context) {
        alert(arg);
    }
    function SaveConserver(idannonce) {
        CallTheServerSaveConserver("E!!!" + idannonce, '');
    }
    function ReceiveServerDataSaveConserver(arg, context) {
        alert(arg);
    }
    function SupprimerAnnonce(idannonce) {
        var answer = confirm("Do you want delete this annonce?");
        if (answer) {
            var theform;

            theform = document.aspnetForm;
            theform.__EVENTTARGET.value = "MyCellButton"; // Hiddin Button ID 

            theform.__EVENTARGUMENT.value = idannonce;
            theform.submit();
        }
        
        
        
    }

    function multiPopup(idannonce) {

        var value;
        value = idannonce;
        document.getElementById('<%=TextBoxIDAnnonceRappeler.ClientID%>').innerText = value; 
        $find('<%=ModalPopupExtender1.ClientID%>').show(); 
        return false;
    }


    function setTextValue(textBoxID, text, panelID) {
        //$get(textBoxID).value = text;
        //$get(panelID).style.display = "None";
    }


    
    </script>
    <style type="text/css">
        #Popup_tooltip
        {
            display :none;
            border:1px solid black; 
            background-color:gray; color:white; 
            text-align: center; 
            top: 325px;
            left: 600px;
            width:200px;
            overflow:visible;
            position:absolute;
        }
        
        #lala
        {
            border: 1px solid Gray;
            background-color:#ffffdd;
            padding:3px;
            width:137px;
            position:absolute;
            top:277px;
            left:617px;
            height: 138px;
        }

        .modalPopup 
        {
            border: 3px solid Gray;
            background-color:#ffffdd; 
            padding:3px; 
            width:426px; 
        }

        .modalBackground 
        {
            background-color:Gray;
            opacity:0.6;
            }
            
        .foo:hover 
        {
            cursor: pointer;
            color:blue;
            text-decoration: underline;
        }
        .action:hover 
        {
            cursor: pointer;
        }
        .linedouble
        {
            color:White;
            background-color:Orange;
        }
        .action
        {
            margin:3px;    
        }
        /*
        .tableAnn 
        { 
            border-collapse:collapse; 
            margin:0; 
            padding:0; 
        }
        .trAnn 
        { 
            border:1px solid #000; 
            margin-bottom:4px; 
            padding:5px; 
        } 
        */
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--<meta http-equiv="CONTENT-TYPE" content="text/html; charset=utf-8"/>-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        
        <asp:Table ID="TableHeader" runat="server" Width="100%" 
    ForeColor="White" style="margin-bottom: 0px" 
        BackImageUrl="~/IMG/bgr_wide_yellow.png" >
            <asp:TableHeaderRow  id ="TableHeaderRow1">
                <asp:TableHeaderCell id ="TableHeaderCellDate" HorizontalAlign="Left" Width="15%">Date</asp:TableHeaderCell>
                <asp:TableHeaderCell id ="TableHeaderCellPoste" HorizontalAlign="Left" Width="30%">Poste</asp:TableHeaderCell>
                <asp:TableHeaderCell id ="TableHeaderCellEntreprise" HorizontalAlign="Left" Width="20%">Entreprise</asp:TableHeaderCell>
                <asp:TableHeaderCell id ="TableHeaderCellLocation" HorizontalAlign="Left" Width="10%">Localisation</asp:TableHeaderCell>
                <asp:TableHeaderCell id ="TableHeaderCellInf" HorizontalAlign="Left" Width="15%">Actions</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <asp:MultiView ID="MultiView1" runat="server" 
        oninit="MultiView1_Init" ActiveViewIndex="0" >
            <asp:View ID="View1" runat="server">
                <asp:Table ID="Table1" class="tableAnn" runat="server" Width="100%"> 
                </asp:Table>
            </asp:View>
        </asp:MultiView>
        <!--onclick="__doPostBack('','')"> -->
    <p style="text-align:center">
    <asp:Button ID="ButtonPreview" runat="server" 
        onclick="ButtonPreview_Click" 
        style="background-image:url('../IMG/Used/arw_nav_prev.png')" Height="20px" 
            Width="21px"/>
    <asp:Button ID="ButtonSuivante" runat="server" onclick="ButtonSuivante_Click" 
            style="background-image:url('../IMG/Used/arw_nav_next.png')" Height="20px" 
            Width="21px"/>
        <br />
    </p>
    <div id="Popup_tooltip" class="tooltip11" >Help contents related to this topic will display here. As a tooltip, if u need for help?</div>
   
    
    
    <asp:Button ID="Button100" runat="server" Text="Button" style="VISIBILITY: hidden"  />

            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
            TargetControlID="Button100"
            PopupControlID="PopupDiv"
            
           
            CancelControlID="CancelButton" 
            BackgroundCssClass="modalBackground"

            >
    </asp:ModalPopupExtender>
        <div id="PopupDiv" class="modalPopup" runat="server">
        <asp:TextBox ID="TextBoxIDAnnonceRappeler" runat="server" style="display:none"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="date: "></asp:Label>
        <asp:TextBox ID="TextBoxDateRappeler"
            runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Format="dd/MM/yyyy"
            Enabled="True" TargetControlID="TextBoxDateRappeler">
        </asp:CalendarExtender>


            <asp:Label ID="Label3" runat="server" Text="Time: "></asp:Label>


            <asp:TextBox ID="TextBox1" runat="server" Width="41px" Height="19px"></asp:TextBox>
 

            <asp:NumericUpDownExtender ID="TextBox1_NumericUpDownExtender" runat="server" 
                Enabled="True" Maximum="23" 
                Minimum="0" RefValues="" ServiceDownMethod="" 
                ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" 
                TargetButtonUpID="" TargetControlID="TextBox1" Width="40">
            </asp:NumericUpDownExtender>
 

            h


            <asp:TextBox ID="TextBox2" runat="server" Width="41px" Height="19px"></asp:TextBox>
 

            <asp:NumericUpDownExtender ID="TextBox2_NumericUpDownExtender" runat="server" 
                Enabled="True" Maximum="23" 
                Minimum="0" RefValues="" ServiceDownMethod="" 
                ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" 
                TargetButtonUpID="" TargetControlID="TextBox2" Width="40">
            </asp:NumericUpDownExtender>

 

            m<br />
        <asp:Button ID="OkButton" runat="server" Text="OK" onclick="OkButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
 
        </div>
</asp:Content>
