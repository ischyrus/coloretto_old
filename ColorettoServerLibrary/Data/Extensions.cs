using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Coloretto.Services.Data
{
    public static class Extensions
    {
        public static MembershipUser ToMembershipUser(this User user)
        {
            MembershipUser mu = new MembershipUser("ColorettaProvider", user.DisplayName, user.Username, user.Email, string.Empty, string.Empty, true, user.Deleted.HasValue, user.Created, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, user.Deleted.HasValue ? user.Deleted.Value : DateTime.MinValue);
            return mu;
        }
    }
}
