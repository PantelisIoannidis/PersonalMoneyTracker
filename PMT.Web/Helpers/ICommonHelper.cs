using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace PMT.Web.Helpers
{
    public interface ICommonHelper
    {
        string GetUserId(HttpContextBase httpContext);
        string GetUserName(HttpContextBase httpContext);
        List<CultureInfo> GetClientCultureInfo(HttpContextBase httpContext);
        List<string> GetUserLanguages(HttpContextBase httpContextBase);
        string GetServerCulture();

        void SetTransactionsPreferences(HttpContextBase httpContext, string transactionFilterPreferences);
        string GetTransactionsPreferences(HttpContextBase httpContext);
        void SetThemePreference(HttpContextBase httpContext, string theme);
        string GetThemePreference(HttpContextBase httpContext);
    }
}