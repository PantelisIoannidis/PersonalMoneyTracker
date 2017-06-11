using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using PMT.Models;
using PMT.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Helpers
{
    public class ThemeFilterAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IUnityContainer container = UnityConfig.GetConfiguredContainer().Resolve<IUnityContainer>();
            var commonHelper = container.Resolve<ICommonHelper>();
            var TempData = filterContext.Controller.TempData;
            var ViewBag = filterContext.Controller.ViewBag;

            string objPreferences = "";
            var themePreferences = new ThemePreferences() { ItemsPerPage = 10, Theme = "Default" };
            var cookieData = commonHelper.GetThemePreference(filterContext.HttpContext);
            var tempData = TempData[GlobalCookies.themePreferenceCookie];
            if (tempData != null)
                objPreferences = (string)tempData;
            else if (cookieData != null)
                objPreferences = cookieData;

            if (!string.IsNullOrEmpty(objPreferences))
                themePreferences = JsonConvert.DeserializeObject<ThemePreferences>(objPreferences);

            ViewBag.ThemeBootstrap = "/Content/bootswatch/" + themePreferences.Theme + "/bootstrap.min.css";
            ViewBag.ThemeCustomCss = "/css/Theme/" + themePreferences.Theme + "Css.css";
            if (string.IsNullOrEmpty(themePreferences.Theme) || themePreferences.Theme == "Default")
            {
                ViewBag.ThemeBootstrap = "/Content/bootstrap.min.css";
                ViewBag.ThemeCustomCss = "/css/Theme/DefaultCss.css";
            }

            ViewBag.ItemsPerPage = themePreferences.ItemsPerPage;
        }
    }
}