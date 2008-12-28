using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Coloretta.Website;
using System.Drawing;

public partial class Signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox_Username.BorderColor = Color.Empty;
        TextBox_Password1.BorderColor = Color.Empty;
        TextBox_Password2.BorderColor = Color.Empty;
    }

    protected void Button_Signin_Click(object sender, EventArgs e)
    {
        string username = TextBox_Username.Text.Trim();
        string password = TextBox_Password.Text;

        if (Membership.ValidateUser(username, password))
        {
            ColorettraMembershipWrapper provider = Membership.Provider as ColorettraMembershipWrapper;
            Coloretto.Services.Data.User user = provider.ActualProvider.GetUser(username, false);
            if (user.Deleted != null)
            {
                Views.ActiveViewIndex = 1;
                Session["DoChangePasswordForUser"] = username;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(username, true);
                Page.Response.Redirect("Home.aspx");
            }
        }
        else
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* Unable to verify username/password." });
        }
    }

    protected void Button_ChangePassword_Click(object sender, EventArgs e)
    {
        if (Session["DoChangePasswordForUser"] == null)
        {
            Views.ActiveViewIndex = 0;
            TextBox_Username.Text = string.Empty;
            TextBox_Password.Text = string.Empty;
            return;
        }

        string username = (string)Session["DoChangePasswordForuser"];
        if (TextBox_Password1.Text.Length < 4)
        {
            TextBox_Password1.BorderColor = Color.Red;
            Password1_Err.Visible = true;
            Password1_Err.Controls.Add(new Literal { Text = "* Passwords must be at least 4 characters." });
            Views.ActiveViewIndex = 1;
            return;
        }
        else if (TextBox_Password1.Text != TextBox_Password2.Text)
        {
            TextBox_Password2.BorderColor = Color.Red;
            Password2_Err.Visible = true;
            Password2_Err.Controls.Add(new Literal { Text = "* This password does not match the first password." });
            Views.ActiveViewIndex = 1;
            return;
        }
        else if(!Membership.Provider.ChangePassword(username, null, TextBox_Password2.Text))
        {
            TextBox_Password1.BorderColor = Color.Red;
            Password1_Err.Visible = true;
            Password1_Err.Controls.Add(new Literal { Text = "* There was a problem setting your password. Try again." });
            Views.ActiveViewIndex = 1;
            return;
        }

        FormsAuthentication.SetAuthCookie(username, true);
        Response.Redirect("Home.aspx");
    }
}
