<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordRecoveryPage.aspx.cs" Inherits="WebApp7.MembershipPages.PasswordRecoveryPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="#e6f0f4">
    <form id="form1" runat="server">
    <div>
    
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
        </asp:PasswordRecovery>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Back to Home Page</asp:HyperLink>
    
    </div>
    </form>
</body>
</html>
