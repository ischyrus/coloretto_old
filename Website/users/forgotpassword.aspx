<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="users_forgotpassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>Coloretta - Forgot Password</title>
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
                    <asp:View ID="RequestView" runat="server">
                        <h2><strong>Forgotten password</strong></h2>
                        <div class="subparagraph">
                            Provider either your username or password and we'll get your going again.
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Reminder" runat="server" Text="Send password reminder" ToolTip="Send me an email reminding me of my password"
                                            OnClick="Button_Reminder_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="RequestSent" runat="server">
                        <div class="subparagraph">
                           An email with the password reminder has been sent to the address on file.    
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
