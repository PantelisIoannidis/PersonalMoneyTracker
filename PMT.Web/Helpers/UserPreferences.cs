using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMT.Web.Helpers
{
    // DATETIME.NOW
    public class UserPreferences
    {
        public const string lastUsedUserAccountCookie = "lastUserUserAccount";

        public void SetLastUsedUserAccount(HttpContextBase httpContext, int userAccountId) {
            HttpCookie cookie =  new HttpCookie(lastUsedUserAccountCookie);
            cookie.Value = userAccountId.ToString();
            cookie.Expires = DateTime.Now.AddDays(30);
            httpContext.Response.Cookies.Add(cookie);
        }

        public int GetLastUsedUserAccount(HttpContextBase httpContext) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(lastUsedUserAccountCookie);
            int userAccount = 0;
            int.TryParse(cookie.Value, out userAccount);
            return userAccount;
        }
    }
}