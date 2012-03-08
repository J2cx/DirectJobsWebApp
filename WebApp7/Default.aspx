<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp7._Default" Trace="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DireceJobs</title>
    <style type="text/css">
        #PanelBackground
        {
            margin: 0 auto;
            Height:768px;
            Width:1024px;
        }
        #PanelLogin
        {
            position : relative;
            height: 149px;
            width: 344px;
            top: 371px;
            left: 591px;
        }
        .tabletxtbox
        {
            background-color: white;
            border-color: white;
            border-style: none;
            border-width: 0px;
            height: 100%;
            width: 85%;
        }
    </style>
</head>
<body style="background-color:#e6f0f4;background-image:url('img/homepageexport.png');background-repeat:no-repeat;background-position:center top;margin:0px">
    <form id="form1" runat="server" style="margin: 0 auto;">
    <asp:Panel ID="PanelBackground" runat="server">
   
       <asp:Panel ID="PanelLogin" runat="server">
            <asp:Login ID="Login1" runat="server" 
                CreateUserText="New User" 
                RememberMeSet="True"
                CreateUserUrl="~/MembershipPages/CreateNewUserPage.aspx" 
                DestinationPageUrl="~/MemberPage/MemberPage1.aspx" 
                PasswordRecoveryText="Forget Password" 
                PasswordRecoveryUrl="~/MembershipPages/PasswordRecoveryPage.aspx" 
                Font-Names="Verdana" Font-Size="0.9em" ForeColor="#333333" 
                Height="128px" Width="341px" BackColor="#4594AF">
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <LabelStyle HorizontalAlign="Right" ForeColor="White" />
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0" style="height:128px;width:341px;">
                                    <tr>
                                        <td align="center" colspan="3" 
                                            style="color:White;font-size:0.9em;font-weight:bold;">
                                            Veuillez entrer vos identifiants pour vous connecter</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color:White;">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Login:</asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" class="tabletxtbox" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color:White;">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mot de passe:</asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password" class="tabletxtbox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                ControlToValidate="Password" ErrorMessage="Password is required." 
                                                ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td width="27%">
                                            <asp:Button ID="Button1" runat="server" BackColor="White"
                                                BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#38ACEC" Font-Bold="True" Text="Connexion" 
                                                ValidationGroup="Login1" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Rester connecté" ForeColor="White" Font-Size="0.7em" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="2">
                                            <asp:HyperLink ID="PasswordRecoveryLink" runat="server" ForeColor="White" Font-Size="0.7em" 
                                                NavigateUrl="~/MembershipPages/PasswordRecoveryPage.aspx">J'ai perdu mon identifiant et/ou mon mot de passe</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="0.9em" ForeColor="#284E98" Font-Bold="True"/>
                <TextBoxStyle Font-Size="0.8em" />
                <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" 
                    ForeColor="White" />
            </asp:Login>
        </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
