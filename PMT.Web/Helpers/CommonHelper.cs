using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Web.WebPages;

namespace PMT.Web.Helpers
{
    public class CommonHelper : ICommonHelper
    {
        public string GetUserId(HttpContextBase httpContext)
        {
           return HttpContext.Current.User.Identity.GetUserId(); 
        }

        public List<CultureInfo> GetClientCultureInfo(HttpContextBase httpContext)
        {
            int i = 0;
            List<CultureInfo> cultures = new List<CultureInfo>();
            foreach(string locale in httpContext.Request.UserLanguages)
            {
                locale.Replace(";", "");
                try {
                    cultures.Add(new CultureInfo(locale, false));
                }
                catch { continue; }
            }
            if (cultures.Count == 0)
                cultures.Add(CultureInfo.CurrentCulture);

            cultures.Add(CultureInfo.InvariantCulture);
            return cultures;
        }

        public string GetServerCulture()
        {
            return CultureInfo.CurrentUICulture.ToString();
        }

        public List<string> GetUserLanguages(HttpContextBase httpContextBase)
        {
            var languages = httpContextBase.Request.UserLanguages.ToList();
            if (languages.Count ==0)
                languages.Add(CultureInfo.CurrentCulture.ToString());
            return languages;
        }
    }
}