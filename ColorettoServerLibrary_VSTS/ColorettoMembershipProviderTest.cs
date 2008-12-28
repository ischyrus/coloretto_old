using Coloretto.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Security;
using System.Reflection;
using Coloretto.Services.Data;
using System.Linq;

namespace ColorettoServerLibrary_VSTS
{
	[TestClass()]
	public class ColorettoMembershipProviderTest
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get;
			set;
		}

		/// <summary>
		///A test for ValidateUser
		///</summary>
		[TestMethod]
		public void ValidateUserTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();

			string username = "ValidUserTest";
			string password = "vut";
			bool expected = true;
			bool actual = target.ValidateUser(username, password);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for ResetPassword
		///</summary>
		[TestMethod()]
		public void ResetPasswordTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "ValidUserTest";
			string answer = string.Empty;
			string expected = "vut";
			string actual = target.ResetPassword(username, answer);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for GetUserNameByEmail
		///</summary>
		[TestMethod()]
		public void GetUserNameByEmailTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string email = "vut@ischyrus.com";
			string expected = "VALIDUSERTEST";
			string actual = target.GetUserNameByEmail(email);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for GetUser
		///</summary>
		[TestMethod()]
		public void GetUserTest1()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			object providerUserKey = "validusertest";
			bool userIsOnline = false;
			User actual = target.GetUser(providerUserKey, userIsOnline);
			Assert.AreEqual(providerUserKey.ToString().ToUpper(), actual.Username);
		}

		/// <summary>
		///A test for GetUser
		///</summary>
		[TestMethod()]
		public void GetUserTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "validusertest";
			bool userIsOnline = false;
			User actual = target.GetUser(username, userIsOnline);
			Assert.AreEqual(username.ToUpper(), actual.Username);
		}

		/// <summary>
		///A test for GetPassword
		///</summary>
		[TestMethod()]
		public void GetPasswordTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "validusertest";
			string answer = string.Empty;
			string actual = target.GetPassword(username, answer);
			Assert.AreEqual("vut", actual);
		}

		/// <summary>
		///A test for GetAllUsers
		///</summary>
		[TestMethod()]
		public void GetAllUsersTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			int pageIndex = 0;
			int pageSize = 10;
			int totalRecords = 0;
			int totalRecordsExpected = 1;
			var actual = target.GetAllUsers(pageIndex, pageSize, out totalRecords).Count();
			Assert.AreEqual(totalRecordsExpected, totalRecords);
			Assert.AreEqual(1, actual);
		}

		/// <summary>
		///A test for FindUsersByName
		///</summary>
		[TestMethod()]
		public void FindUsersByNameTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string usernameToMatch = "ValidUserTest";
			int pageIndex = 0;
			int pageSize = 10;
			int totalRecords = 0;
			int totalRecordsExpected = 1;
			var actual = target.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords).Count();
			Assert.AreEqual(totalRecordsExpected, totalRecords);
			Assert.AreEqual(1, actual);
		}

		/// <summary>
		///A test for DeleteUser
		///</summary>
		[TestMethod()]
		public void DeleteUserTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "TempTestUser1";

			MembershipCreateStatus status = MembershipCreateStatus.Success;
			Assert.IsNotNull(target.CreateUser(username, username, username, null, null, true, username, out status), "Unable to create test user.");

			bool deleteAllRelatedData = true;
			bool expected = true;
			bool actual = target.DeleteUser(username, deleteAllRelatedData);
			Assert.AreEqual(expected, actual);
			Assert.IsNull(target.GetUser(username, false), "Unable to clean up test user.");
		}

		/// <summary>
		///A test for CreateUser
		///</summary>
		[TestMethod()]
		public void CreateUserTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "TempTestUser2";
			string password = username;
			string email = username;
			MembershipCreateStatus status = MembershipCreateStatus.Success;
			MembershipCreateStatus statusExpected = MembershipCreateStatus.Success;
			User actual = target.CreateUser(username, password, email, null, null, true, username, out status);

			Assert.IsTrue(target.DeleteUser(username, true), "Unable to clean up test user.");

			Assert.IsNotNull(actual);
			Assert.AreEqual(statusExpected, status);
			Assert.AreEqual(username, actual.DisplayName);
			Assert.AreEqual(username.ToUpper(), actual.Username);
			Assert.AreEqual(password, actual.Password);
		}

		/// <summary>
		///A test for ChangePassword
		///</summary>
		[TestMethod()]
		public void ChangePasswordTest()
		{
			ColorettoMembershipProvider target = new ColorettoMembershipProvider();
			string username = "TempTestUser3";
			string oldPassword = "ttu3";

			MembershipCreateStatus status = MembershipCreateStatus.Success;
			Assert.IsNotNull(target.CreateUser(username, oldPassword, username, null, null, true, username, out status), "Unable to create test user.");
			Assert.IsTrue(target.ValidateUser(username, oldPassword));

			string newPassword = "tzu3";
			bool actual = target.ChangePassword(username, oldPassword, newPassword);
			bool isValid = target.ValidateUser(username, newPassword);

			Assert.IsTrue(target.DeleteUser(username, true), "Unable to cleaup test.");

			Assert.IsTrue(actual);
			Assert.IsTrue(isValid);
		}
	}
}
