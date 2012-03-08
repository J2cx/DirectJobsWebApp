<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MemberPage/MemberPages.Master" CodeBehind="Profile.aspx.cs" Inherits="WebApp7.MemberPage.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/MemberPage/ChangePasswordPage.aspx">Change Pass Word</asp:HyperLink>
        <br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
    
</asp:Content>