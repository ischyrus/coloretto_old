using Coloretto.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Security;

namespace CardManagementVSTS
{
    
    
    /// <summary>
    ///This is a test class for ColorettoMembershipProviderTest and is intended
    ///to contain all ColorettoMembershipProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ColorettoMembershipProviderTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ValidateUser
        ///</summary>
        [TestMethod()]
        public void ValidateUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateUser(username, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            MembershipUser user = null; // TODO: Initialize to an appropriate value
            target.UpdateUser(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UnlockUser
        ///</summary>
        [TestMethod()]
        public void UnlockUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.UnlockUser(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        public void ResetPasswordTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string answer = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ResetPassword(username, answer);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserNameByEmail
        ///</summary>
        [TestMethod()]
        public void GetUserNameByEmailTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetUserNameByEmail(email);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserTest1()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            object providerUserKey = null; // TODO: Initialize to an appropriate value
            bool userIsOnline = false; // TODO: Initialize to an appropriate value
            MembershipUser expected = null; // TODO: Initialize to an appropriate value
            MembershipUser actual;
            actual = target.GetUser(providerUserKey, userIsOnline);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            bool userIsOnline = false; // TODO: Initialize to an appropriate value
            MembershipUser expected = null; // TODO: Initialize to an appropriate value
            MembershipUser actual;
            actual = target.GetUser(username, userIsOnline);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPassword
        ///</summary>
        [TestMethod()]
        public void GetPasswordTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string answer = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetPassword(username, answer);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetNumberOfUsersOnline
        ///</summary>
        [TestMethod()]
        public void GetNumberOfUsersOnlineTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetNumberOfUsersOnline();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAllUsers
        ///</summary>
        [TestMethod()]
        public void GetAllUsersTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            int pageIndex = 0; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int totalRecords = 0; // TODO: Initialize to an appropriate value
            int totalRecordsExpected = 0; // TODO: Initialize to an appropriate value
            MembershipUserCollection expected = null; // TODO: Initialize to an appropriate value
            MembershipUserCollection actual;
            actual = target.GetAllUsers(pageIndex, pageSize, out totalRecords);
            Assert.AreEqual(totalRecordsExpected, totalRecords);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FindUsersByName
        ///</summary>
        [TestMethod()]
        public void FindUsersByNameTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string usernameToMatch = string.Empty; // TODO: Initialize to an appropriate value
            int pageIndex = 0; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int totalRecords = 0; // TODO: Initialize to an appropriate value
            int totalRecordsExpected = 0; // TODO: Initialize to an appropriate value
            MembershipUserCollection expected = null; // TODO: Initialize to an appropriate value
            MembershipUserCollection actual;
            actual = target.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
            Assert.AreEqual(totalRecordsExpected, totalRecords);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FindUsersByEmail
        ///</summary>
        [TestMethod()]
        public void FindUsersByEmailTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string emailToMatch = string.Empty; // TODO: Initialize to an appropriate value
            int pageIndex = 0; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int totalRecords = 0; // TODO: Initialize to an appropriate value
            int totalRecordsExpected = 0; // TODO: Initialize to an appropriate value
            MembershipUserCollection expected = null; // TODO: Initialize to an appropriate value
            MembershipUserCollection actual;
            actual = target.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
            Assert.AreEqual(totalRecordsExpected, totalRecords);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUser
        ///</summary>
        [TestMethod()]
        public void DeleteUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            bool deleteAllRelatedData = false; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DeleteUser(username, deleteAllRelatedData);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateUser
        ///</summary>
        [TestMethod()]
        public void CreateUserTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string passwordQuestion = string.Empty; // TODO: Initialize to an appropriate value
            string passwordAnswer = string.Empty; // TODO: Initialize to an appropriate value
            bool isApproved = false; // TODO: Initialize to an appropriate value
            object providerUserKey = null; // TODO: Initialize to an appropriate value
            MembershipCreateStatus status = new MembershipCreateStatus(); // TODO: Initialize to an appropriate value
            MembershipCreateStatus statusExpected = new MembershipCreateStatus(); // TODO: Initialize to an appropriate value
            MembershipUser expected = null; // TODO: Initialize to an appropriate value
            MembershipUser actual;
            actual = target.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
            Assert.AreEqual(statusExpected, status);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangePasswordQuestionAndAnswer
        ///</summary>
        [TestMethod()]
        public void ChangePasswordQuestionAndAnswerTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string newPasswordQuestion = string.Empty; // TODO: Initialize to an appropriate value
            string newPasswordAnswer = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string oldPassword = string.Empty; // TODO: Initialize to an appropriate value
            string newPassword = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangePassword(username, oldPassword, newPassword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ColorettoMembershipProvider Constructor
        ///</summary>
        [TestMethod()]
        public void ColorettoMembershipProviderConstructorTest()
        {
            ColorettoMembershipProvider target = new ColorettoMembershipProvider();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
