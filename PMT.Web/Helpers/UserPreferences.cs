using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMT.Web.Helpers
{
    // DATETIME.NOW
    public class UserPreferences
    {
        public const string lastUsedMoneyAccountCookie = "lastUserMoneyAccount";

        public void SetLastUsedMoneyAccount(HttpContextBase httpContext, int MoneyAccountId) {
            HttpCookie cookie =  new HttpCookie(lastUsedMoneyAccountCookie);
            cookie.Value = MoneyAccountId.ToString();
            cookie.Expires = DateTime.Now.AddDays(30);
            httpContext.Response.Cookies.Add(cookie);
        }

        public int GetLastUsedMoneyAccount(HttpContextBase httpContext) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(lastUsedMoneyAccountCookie);
            int MoneyAccount = 0;
            int.TryParse(cookie.Value, out MoneyAccount);
            return MoneyAccount;
        }
    }
}