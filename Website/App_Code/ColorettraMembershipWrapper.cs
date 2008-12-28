using System.Web.Security;
using Coloretto.Services;
using Coloretto.Services.Data;

namespace Coloretta.Website
{
	/// <summary>
	/// This is a wrapper for the ColorettaMembership. Unfortunately ASP.NET can only load custom membership providers taht are located inside of the website itself.
	/// </summary>
	public class ColorettraMembershipWrapper : MembershipProvider
	{
		private ColorettoMembershipProvider _provider = new ColorettoMembershipProvider();

		public ColorettoMembershipProvider ActualProvider { get { return _provider; } }

		public override string ApplicationName
		{
			get
			{
				return _provider.ApplicationName;
			}
			set
			{
				_provider.ApplicationName = value;
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			return _provider.ChangePassword(username, oldPassword, newPassword);
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			return _provider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			User user = _provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
			if (user != null)
				return user.ToMembershipUser();
			else
				return null;
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			return _provider.DeleteUser(username, deleteAllRelatedData);
		}

		public override bool EnablePasswordReset
		{
			get { return _provider.EnablePasswordReset; }
		}

		public override bool EnablePasswordRetrieval
		{
			get { return _provider.EnablePasswordRetrieval; }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			var users = _provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
			MembershipUserCollection collection = new MembershipUserCollection();
			foreach (User user in users) { collection.Add(user.ToMembershipUser()); }
			return collection;
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			var users = _provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
			MembershipUserCollection collection = new MembershipUserCollection();
			foreach (User user in users) { collection.Add(user.ToMembershipUser()); }
			return collection;
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			var users = _provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
			MembershipUserCollection collection = new MembershipUserCollection();
			foreach (User user in users) { collection.Add(user.ToMembershipUser()); }
			return collection;
		}

		public override int GetNumberOfUsersOnline()
		{
			return _provider.GetNumberOfUsersOnline();
		}

		public override string GetPassword(string username, string answer)
		{
			return _provider.GetPassword(username, answer);
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			User user = _provider.GetUser(username, userIsOnline);
			if (user == null)
				return null;
			else
				return user.ToMembershipUser();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			return GetUser(providerUserKey.ToString(), userIsOnline);
		}

		public override string GetUserNameByEmail(string email)
		{
			return _provider.GetUserNameByEmail(email);
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { return _provider.MaxInvalidPasswordAttempts; }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { return _provider.MinRequiredNonAlphanumericCharacters; }
		}

		public override int MinRequiredPasswordLength
		{
			get { return _provider.MinRequiredPasswordLength; }
		}

		public override int PasswordAttemptWindow
		{
			get { return _provider.PasswordAttemptWindow; }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { return _provider.PasswordFormat; }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { return _provider.PasswordStrengthRegularExpression; }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { return _provider.RequiresQuestionAndAnswer; }
		}

		public override bool RequiresUniqueEmail
		{
			get { return _provider.RequiresUniqueEmail; }
		}

		public override string ResetPassword(string username, string answer)
		{
			return _provider.ResetPassword(username, answer);
		}

		public override bool UnlockUser(string userName)
		{
			return _provider.UnlockUser(userName);
		}

		public override void UpdateUser(MembershipUser user)
		{
			return;
		}

		public override bool ValidateUser(string username, string password)
		{
			return _provider.ValidateUser(username, password);
		}
	}

}