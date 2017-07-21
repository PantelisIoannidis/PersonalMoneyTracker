using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace PMT.Web.Helpers
{
    public interface ICommonHelper
    {
        void ApplyServerCulture(HttpContextBase httpContext);
        List<CultureInfo> GetClientCultureInfo(HttpContextBase httpContext);
        string GetDisplayLanguage(HttpContextBase httpContext);
        string GetLanguageFormatting(HttpContextBase httpContext);
        string GetThemePreference(HttpContextBase httpContext);
        string GetTransactionsPreferences(HttpContextBase httpContext);
        string GetUserId(HttpContextBase httpContext);
        string GetUserName(HttpContextBase httpContext);
        void SetServerCulture(HttpContextBase httpContext, string displayLanguage, string languageFormatting);
        void SetThemePreference(HttpContextBase httpContext, string theme);
        void SetTransactionsPreferences(HttpContextBase httpContext, string preferences);
        string GetTimeZoneOffset(HttpContextBase httpContext);
    }
}