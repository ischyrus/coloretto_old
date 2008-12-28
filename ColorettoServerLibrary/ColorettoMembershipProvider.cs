using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using Coloretto.Services.Data;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Coloretto.Services
{
	public class ColorettoMembershipProvider : UserNamePasswordValidator
	{
		public string ApplicationName
		{
			get;
			set;
		}

		public bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				User user = context.Users.Where(u => u.Username.Equals(username.ToUpper()) || u.Email.Equals(username.ToUpper()))
										 .SingleOrDefault();
				if (user == null)
				{
					return false;
				}
				else
				{
					if (user.Deleted == user.Created) { user.Deleted = null; }
					user.Password = newPassword;
					context.SubmitChanges();
					return true;
				}
			}
		}
		public bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				string em = email.Trim();
				string uname = username.Trim();
				string passwd = password.Trim();

				if (uname.Length < 3)
				{
					status = MembershipCreateStatus.InvalidUserName;
					return null;
				}
				if (context.Users.Where(u => u.Username.Equals(uname.ToUpper()) || u.Email.Equals(uname.ToUpper())).Any())
				{
					status = MembershipCreateStatus.DuplicateUserName;
					return null;
				}
				else if (context.Users.Where(u => u.Email.Equals(em.ToUpper()) || u.Email.Equals(uname.ToUpper())).Any())
				{
					status = MembershipCreateStatus.DuplicateEmail;
					return null;
				}

				try
				{
					DateTime creationDate = DateTime.Now;
					User user = new User
							{
								Username = username.ToUpper(),
								Password = password,
								Email = email,
								Newsletter = true,
								Created = creationDate,
								Deleted = creationDate,
								DisplayName = username
							};
					context.Users.InsertOnSubmit(user);
					status = MembershipCreateStatus.Success;
					context.SubmitChanges();

					return user;
				}
				catch
				{
					Debug.Fail("Unable to insert player into database.");
					status = MembershipCreateStatus.UserRejected;
					return null;
				}
			}
		}

		public bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				var user = context.Users.Where(u => u.Email.Equals(username.ToUpper().Trim()) || u.Username.Equals(username.ToUpper().Trim()))
										.FirstOrDefault();
				if (user == null)
				{
					return false;
				}

				try
				{
					if (deleteAllRelatedData)
					{
						context.Users.DeleteOnSubmit(user);
						context.SubmitChanges();
						return true;
					}
					else if (user.Deleted.HasValue == false)
					{
						user.Deleted = DateTime.UtcNow;
						context.SubmitChanges();
						return true;
					}
					else
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					Debug.Fail("Unable to delete user as requested. (Full data purce == " + deleteAllRelatedData, ex.ToString());
					return false;
				}
			}
		}

		public bool EnablePasswordReset
		{
			get { return false; }
		}

		public bool EnablePasswordRetrieval
		{
			get { return true; }
		}

		public IEnumerable<User> FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				int userCount = context.Users.Where(u => u.Email.StartsWith(emailToMatch.ToUpper())).Count();
				totalRecords = userCount;

				int actualSkip = Math.Max(userCount, pageSize * pageIndex);
				int actualTake = Math.Min(userCount - (pageSize * pageIndex), pageSize);

				var users = context.Users.Where(u => u.Email.StartsWith(emailToMatch.ToUpper()))
								   .Skip(actualSkip)
								   .Take(actualTake);

				return users.ToList();
			}
		}

		public IEnumerable<User> FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				int userCount = context.Users.Where(u => u.Username.StartsWith(usernameToMatch.ToUpper())).Count();
				totalRecords = userCount;

				if (totalRecords == 0)
				{
					return new List<User>(0);
				}

				// TODO: I know I'm not covering all scenerios. For now there will just be exceptions if I make stupid requests.
				int actualSkip = Math.Min(userCount, pageSize * pageIndex);
				int actualTake = pageSize;
				if (pageSize > totalRecords)
					actualTake = totalRecords;

				var users = context.Users.Where(u => u.Username.StartsWith(usernameToMatch.ToUpper()))
								   .Skip(actualSkip)
								   .Take(actualTake);

				return users.ToList();
			}
		}

		public IEnumerable<User> GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				context.ObjectTrackingEnabled = false;
				int userCount = context.Users.Where(u => u.Deleted == null).Count();
				totalRecords = userCount;

				int actualSkip = Math.Min(userCount, pageSize * pageIndex);
				int actualTake = Math.Min(userCount - (pageSize * pageIndex), pageSize);

				var users = context.Users.Where(u => u.Deleted == null).OrderBy(u => u.Username)
								   .Skip(actualSkip)
								   .Take(actualTake);

				return users.ToList();
			}
		}

		public int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public string GetPassword(string username, string answer)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				User user = context.Users.Where(u => u.Username.Equals(username.ToUpper().Trim()) || u.Email.Equals(username.ToUpper().Trim())).SingleOrDefault();
				if (user == null)
				{
					return null;
				}
				else
				{
					return user.Password;
				}
			}
		}

		public User GetUser(string username, bool userIsOnline)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				User user = context.Users.Where(u => u.Username.Equals(username.ToUpper().Trim()) || u.Email.Equals(username.ToUpper().Trim())).SingleOrDefault();
				return user;
			}
		}

		public User GetUser(object providerUserKey, bool userIsOnline)
		{
			return GetUser(providerUserKey.ToString(), userIsOnline);
		}

		public string GetUserNameByEmail(string email)
		{
			return GetUser(email, false).Username;
		}

		public int MaxInvalidPasswordAttempts
		{
			get { return 10; }
		}

		public int MinRequiredNonAlphanumericCharacters
		{
			get { return 0; }
		}

		public int MinRequiredPasswordLength
		{
			get { return 4; }
		}

		public int PasswordAttemptWindow
		{
			get { return 60; }
		}

		public MembershipPasswordFormat PasswordFormat
		{
			get { return MembershipPasswordFormat.Clear; }
		}

		public string PasswordStrengthRegularExpression
		{
			get { return null; }
		}

		public bool RequiresQuestionAndAnswer
		{
			get { return false; }
		}

		public bool RequiresUniqueEmail
		{
			get { return true; }
		}

		public string ResetPassword(string username, string answer)
		{
			return GetPassword(username, answer);
		}

		public bool UnlockUser(string userName)
		{
			return false;
		}

		public bool ValidateUser(string username, string password)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				User user = context.Users.Where(u => u.Username.Equals(username.ToUpper().Trim()) || u.Email.Equals(username.ToUpper().Trim())).SingleOrDefault();
				if (user == null || (user.Deleted != null && user.Deleted != user.Created))
				{
					return false;
				}
				else
				{
					bool valid = user.Password.Equals(password.Trim());
					return valid;
				}
			}
		}

		public override void Validate(string userName, string password)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				User user = GetUser(userName, false);
				if (user.Password == password && (user.Deleted == null || user.Deleted.Value == user.Created))
				{
					try
					{
						AccessLog log = new AccessLog { Username = userName, AccessPoint = "WCF Service", LoginTime = DateTime.Now, Origination = "Unknown", Success = true, AccessLogId = Guid.NewGuid() };
						context.AccessLogs.InsertOnSubmit(log);
						context.SubmitChanges();
					}
					catch (Exception)
					{
					}
				}
				else
				{
					AccessLog log = new AccessLog { Username = userName, AccessPoint = "WCF Service", LoginTime = DateTime.Now, Origination = "Unknown", Success = false, Duration = new DateTimeOffset(DateTime.Now, TimeSpan.FromMilliseconds(0)) };
					context.AccessLogs.InsertOnSubmit(log);
					context.SubmitChanges();
					throw new FaultException(new FaultReason("Unable to verify username and password information."));
				}
			}
		}
	}
}
