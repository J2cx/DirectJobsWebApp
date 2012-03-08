<%@ Page Title="" Language="C#" MasterPageFile="~/MemberPage/MemberPages.Master" AutoEventWireup="true" CodeBehind="AnnoncePage.aspx.cs" Inherits="WebApp7.MemberPage.AnnoncePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h2 id="titleAnnocne" runat="server"></h2>
<div>
    <table width="100%">
        <tr>
            <td>
            Poste:
            </td>
            <td id="tdPoste" runat="server"></td>
            <td>
            Date:
            </td>
            <td id="tdDate" runat="server"></td>
        </tr>
        <tr>
            <td>
            Localisation:
            </td>
            <td id="tdLocalisation" runat="server"></td>
            <td>
            Contrat:
            </td>
            <td id="tdContrat" runat="server"></td>
        </tr>
    </table>
    <p> Description :</p>
    <p id="pDescription" runat="server"></p>
</div>
</asp:Content>
