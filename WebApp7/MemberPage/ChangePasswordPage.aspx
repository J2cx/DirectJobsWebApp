<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MemberPage/MemberPages.Master" CodeBehind="ChangePasswordPage.aspx.cs" Inherits="WebApp7.MemberPage.ChangePasswordPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
        <asp:ChangePassword ID="ChangePassword1" runat="server" 
            ContinueDestinationPageUrl="~/MemberPage/MemberPage1.aspx" 
    CancelDestinationPageUrl="~/MemberPage/MemberPage1.aspx">
        </asp:ChangePassword>
    
</asp:Content>
