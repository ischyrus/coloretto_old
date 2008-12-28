using System;
using System.Drawing;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Coloretto.Services.Data;
using System.IO;
using System.Web.Mail;
using System.Net.Mail;

public partial class users_signup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!Request.IsSecureConnection)
            //{
            //    string url = Request.Url.ToString();
            //    url = url.Insert(4, "s");
            //    Response.Redirect(url);
            //}
        }

        TextBox_Username.BorderColor = Color.Empty;
        TextBox_Email.BorderColor = Color.Empty;
    }

    protected void Button_Signup_Click(object sender, EventArgs e)
    {
        bool hasProblem = false;
        if (TextBox_Username.Text.Trim().Length == 0)
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* Username is a required field." });
            hasProblem = true;
        }

        if (TextBox_Email.Text.Trim().Length == 0)
        {
            TextBox_Email.BorderColor = Color.Red;
            Panel_EmailMessage.Visible = true;
            Panel_EmailMessage.Controls.Add(new Literal { Text = "* Email is a required field." });
            hasProblem = true;
        }
        else if (TextBox_Email.Text.Trim().Length < 3 || !TextBox_Email.Text.Trim().Contains('@') || !TextBox_Email.Text.Trim().Contains('.'))
        {
            TextBox_Email.BorderColor = Color.Red;
            Panel_EmailMessage.Visible = true;
            Panel_EmailMessage.Controls.Add(new Literal { Text = "* An email is required to receive a password." });
            hasProblem = true;
        }

        if (hasProblem)
        {
            return;
        }

        string password = System.Guid.NewGuid().ToString();
        MembershipCreateStatus status = MembershipCreateStatus.Success;
        MembershipUser user = Membership.CreateUser(TextBox_Username.Text, password, TextBox_Email.Text, null,null,true, out status);
        if (status == MembershipCreateStatus.DuplicateEmail)
        {
            TextBox_Email.BorderColor = Color.Red;
            Panel_EmailMessage.Visible = true;
            Panel_EmailMessage.Controls.Add(new Literal { Text = "* This email address already has an account." });
        }
        else if (status == MembershipCreateStatus.DuplicateUserName)
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* Username is already taken." });
        }
        else if (status != MembershipCreateStatus.Success)
        {
            TextBox_Username.BorderColor = Color.Red;
            Panel_UsernameMessage.Visible = true;
            Panel_UsernameMessage.Controls.Add(new Literal { Text = "* There was a problem creating this user." });
        }
        else
        {
            if (!CheckBox_Updates.Checked)
            {
				using (ColorettaDataContext context = new ColorettaDataContext())
                {
                    User udb = context.Users.Where(u => u.Username.Equals(user.UserName.ToUpper())).Single();
                    udb.Newsletter = false;
                    context.SubmitChanges();
                }
            }

            string emailBody = string.Format(File.ReadAllText(MapPath("~/users/WelcomeEmail.txt")), TextBox_Username.Text, TextBox_Email.Text, password);
            SmtpClient client = new SmtpClient();
            client.Send("ischyrus@comcast.net", TextBox_Email.Text, "Coloretta account information", emailBody);

            MultiView1.ActiveViewIndex = 1;
        }
    }
}
