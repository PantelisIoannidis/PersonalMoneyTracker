using PMT.Web.Helpers;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorExtendedAttribute());
            filters.Add(new ThemeFilterAttribute());
        }
    }
}
