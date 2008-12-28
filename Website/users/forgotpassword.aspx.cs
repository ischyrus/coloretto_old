using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class users_forgotpassword : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox_Username.BorderColor = Color.Empty;
    }

    protected void Button_Reminder_Click(object sender, EventArgs e)
    {
        if (TextBox_Username.Text.Trim().Length == 0)
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* This is clearly a required field." });
            return;
        }

        MembershipUser user = Membership.GetUser(TextBox_Username.Text.Trim());
        if (user == null)
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* Sorry, I'm not able to find you!" });
            return;
        }
        else
        {
            string password = Membership.Provider.GetPassword(user.UserName, null);
            string emailBody = string.Format(File.ReadAllText(MapPath("~/users/ForgotPasswordEmail.txt")), user.UserName, user.Email, password);
            SmtpClient client = new SmtpClient();
            client.Send("ischyrus@comcast.net", user.Email, "Coloretta account information", emailBody);

            Views.ActiveViewIndex = 1;
        }
    }
}
