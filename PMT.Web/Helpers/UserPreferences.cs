using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMT.Web.Helpers
{
    // DATETIME.NOW
    public class UserPreferences : IUserPreferences
    {
        public const string lastUsedMoneyAccountCookie = "lastUserMoneyAccount";
        public const string lastUsedPeriodCookie = "lastUsedPeriodCookie";

        public void SetLastUsedMoneyAccount(HttpContextBase httpContext, string moneyAccountObj) {
            HttpCookie cookie =  new HttpCookie(lastUsedMoneyAccountCookie);
            cookie.Value = moneyAccountObj;
            cookie.Expires = DateTime.Now.AddDays(30);
            httpContext.Response.Cookies.Add(cookie);
        }

        public string GetLastUsedMoneyAccount(HttpContextBase httpContext) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(lastUsedMoneyAccountCookie);
            return cookie.Value;
        }

        public void SetTransactionPreferences(HttpContextBase httpContext, string transactionFilterPreferences)
        {
            HttpCookie cookie = new HttpCookie(lastUsedPeriodCookie);
            cookie.Value = transactionFilterPreferences;
            cookie.Expires = DateTime.Now.AddDays(30);
            httpContext.Response.Cookies.Add(cookie);
        }

        public string GetTransactionPreferences(HttpContextBase httpContext)
        {
            HttpCookie cookie = new HttpCookie(lastUsedPeriodCookie);
            return cookie.Value;
        }
    }
}