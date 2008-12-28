<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signin.aspx.cs" Inherits="Signin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>Coloretta - Signin</title>
    <link href="../Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div id="header">
            <ul id="nav">
                <li><a href="../">Home</a></li>
                <li><a href="../Download" title="Rules">Download</a></li>
                <li><a href="../rules/" title="Rules">Rules</a></li>
                <li><a href="../about/" title="About">About</a></li>
            </ul>
            <h1>
                It&#39;s all about cards, colors, and chance.</h1>
        </div>
        <div class="content">
            <div id="content">
                <br />
                <asp:MultiView ID="Views" runat="server" ActiveViewIndex="0" EnableViewState="false">
                    <asp:View ID="SigninView" runat="server">
                        <h2><strong>Signin:</strong></h2>
                        <div class="subparagraph">
                            <table style="margin-top: 10px">
                                <tr>
                                    <td style="text-align: right; vertical-align: top;">
                                        Username or Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Username" runat="server" Width="248px" /><asp:Panel ID="Panel_UsernameMessage"
                                            runat="server" EnableViewState="false" CssClass="errMsg" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Password" runat="server" TextMode="Password" Width="248px" /><asp:Panel ID="Panel_PasswordMessage"
                                            runat="server" EnableViewState="false" CssClass="errMsg" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Signin" runat="server" Text="Signin" 
                                            ToolTip="Submit username and password to sign in." 
                                            onclick="Button_Signin_Click"  />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="ChangePasswordView" runat="server">
                        <h2><strong>Change Password:</strong></h2>
                        <div class="subparagraph">
                            Almost there! Just set your password to something that you'll actually be able to remember.
                             <table style="margin-top: 10px">
                                <tr>
                                    <td>
                                        New Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Password1" runat="server" TextMode="Password" Width="248px" /><asp:Panel ID="Password1_Err"
                                            runat="server" EnableViewState="false" CssClass="errMsg" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Retype Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Password2" runat="server" TextMode="Password" Width="248px" /><asp:Panel ID="Password2_Err"
                                            runat="server" EnableViewState="false" CssClass="errMsg" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_ChangePassword" runat="server" 
                                            Text="Change password and signin" ToolTip="Change password and signin." 
                                            onclick="Button_ChangePassword_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
        <div id="sidebar">
            <div id="title">
                System Requirements</div>
            <div id="titledcontent">
                Windows XP or&nbsp; Windows Vista<br />
                .NET Framework 3.5<br />
                Internet for network play</div>
            <div id="title">
                Links</div>
            <div id="titledcontent">
                <a href="../rules/">How to play</a><br />
                Forums<br />
                <a href="../donate/">Donate via PayPal</a></div>
        </div>
        <div id="footer">
            Coloretta does not cost anything. C<span class="content">oloretta </span>is in not
            endorsed by or affiliated with Coloretto. If you don&#39;t already own it, it is
            definitely worth <a href="http://www.funagain.com/control/product/~product_id=014604"
                style="color: black; text-decoration: underline;">picking up a copy</a>.
        </div>
    </div>
    </form>
</body>
</html>
