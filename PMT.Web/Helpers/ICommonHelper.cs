using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace PMT.Web.Helpers
{
    public interface ICommonHelper
    {
        string GetUserId(HttpContextBase httpContext);
        List<CultureInfo> GetClientCultureInfo(HttpContextBase httpContext);
        List<string> GetUserLanguages(HttpContextBase httpContextBase);
        string GetServerCulture();
    }
}