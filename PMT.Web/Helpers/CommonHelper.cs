using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Web.WebPages;
using static PMT.Entities.Literals;
using PMT.Entities;
using PMT.Common;
using System.Threading;

namespace PMT.Web.Helpers
{
    public class CommonHelper : ICommonHelper
    {
        public string GetUserId(HttpContextBase httpContext)
        {
           return HttpContext.Current.User.Identity.GetUserId(); 
        }
        public string GetUserName(HttpContextBase httpContext)
        {
            return HttpContext.Current.User.Identity.GetUserName();
        }

        public List<CultureInfo> GetClientCultureInfo(HttpContextBase httpContext)
        {
            List<CultureInfo> cultures = new List<CultureInfo>();
            var clientLanguages = httpContext.Request.UserLanguages.Select(c=>c.Split(';')[0]).ToList();
            var implementedLanguages = CultureHelper.GetImplementedDisplayLanguages().Keys.Select(c => c).ToList();
            clientLanguages.AddRange(implementedLanguages);
            clientLanguages = clientLanguages.Distinct().ToList();
            foreach (string locale in clientLanguages)
            {
                try {
                    cultures.Add(new CultureInfo(locale, false));
                }
                catch(Exception ex)
                { continue; }
            }
            if (cultures.Count == 0)
                cultures.Add(CultureInfo.CurrentCulture);

            return cultures;
        }

        public string GetDisplayLanguage(HttpContextBase httpContext)
        {
            string cultureName = null;

            HttpCookie cultureCookie = httpContext.Request.Cookies[Literals.GlobalCookies.DisplayLanguageCookie];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = httpContext.Request.UserLanguages != null && httpContext.Request.UserLanguages.Length > 0 ?
                        httpContext.Request.UserLanguages[0] :
                        null;

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            return cultureName;
        }

        public string GetLanguageFormatting(HttpContextBase httpContext)
        {
            string cultureName = null;

            HttpCookie cultureCookie = httpContext.Request.Cookies[Literals.GlobalCookies.LanguageFormatting];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = httpContext.Request.UserLanguages != null && httpContext.Request.UserLanguages.Length > 0 ?
                        httpContext.Request.UserLanguages[0] :
                        null;

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            return cultureName;
        }

        public void SetServerCulture(HttpContextBase httpContext,string displayLanguage,string languageFormatting)
        {

            if (!string.IsNullOrEmpty(displayLanguage))
            {
                HttpCookie cultureCookie = httpContext.Request.Cookies[Literals.GlobalCookies.DisplayLanguageCookie];
                if (cultureCookie != null)
                {
                    cultureCookie.Value = displayLanguage;
                }
                else
                {
                    cultureCookie = new HttpCookie(Literals.GlobalCookies.DisplayLanguageCookie);
                    cultureCookie.Value = displayLanguage;
                    cultureCookie.Expires = DateTime.Now.AddYears(1);
                }
                cultureCookie.HttpOnly = true;
                httpContext.Response.SetCookie(cultureCookie);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(displayLanguage);
            }
            if (!string.IsNullOrEmpty(languageFormatting))
            {
                HttpCookie cultureCookie = httpContext.Request.Cookies[Literals.GlobalCookies.LanguageFormatting];
                if (cultureCookie != null)
                {
                    cultureCookie.Value = languageFormatting;
                }
                else
                {
                    cultureCookie = new HttpCookie(Literals.GlobalCookies.LanguageFormatting);
                    cultureCookie.Value = languageFormatting;
                    cultureCookie.Expires = DateTime.Now.AddYears(1);
                }
                cultureCookie.HttpOnly = true;
                httpContext.Response.SetCookie(cultureCookie);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageFormatting);
            }
        }

        public void ApplyServerCulture(HttpContextBase httpContext)
        {
            var languageFormatting = GetLanguageFormatting(httpContext);
            var displayLanguage = GetDisplayLanguage(httpContext);
            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageFormatting);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(displayLanguage);
        }

        public void SetTransactionsPreferences(HttpContextBase httpContext, string preferences)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalCookies.transactionsPreferencesCookie] ?? new HttpCookie(GlobalCookies.transactionsPreferencesCookie);
            httpContext.Request.Cookies.Remove(GlobalCookies.transactionsPreferencesCookie);
            cookie.Value = preferences;
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.HttpOnly = true;
            httpContext.Response.SetCookie(cookie);
        }

        public string GetTransactionsPreferences(HttpContextBase httpContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalCookies.transactionsPreferencesCookie] ?? new HttpCookie(GlobalCookies.transactionsPreferencesCookie);
            return cookie.Value;
        }

        public void SetThemePreference(HttpContextBase httpContext, string theme)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalCookies.themePreferenceCookie] ?? new HttpCookie(GlobalCookies.themePreferenceCookie);
            httpContext.Request.Cookies.Remove(GlobalCookies.themePreferenceCookie);
            cookie.Value = theme;
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.HttpOnly = true;
            httpContext.Response.SetCookie(cookie);
        }

        public string GetThemePreference(HttpContextBase httpContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalCookies.themePreferenceCookie] ?? new HttpCookie(GlobalCookies.themePreferenceCookie);
            return cookie.Value;
        }

        public string GetTimeZoneOffset(HttpContextBase httpContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalCookies.TimeZoneCookie] ?? new HttpCookie(GlobalCookies.TimeZoneCookie);
            return cookie.Value;
        }
    }
}