using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.Models;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Controllers
{
    public class BaseController : Controller
    {
        public string tempDataPreferences { get; set; }

        public string userId;
        public string ThemeBootstrap { get; set; }
        public string ThemeCustomCss { get; set; }
        public int ItemsPerPage { get; set; }
        ILogger logger;
        ICommonHelper commonHelper;
        public BaseController(
            ILoggerFactory logger,
            ICommonHelper commonHelper)
        {
            this.commonHelper = commonHelper;
            this.logger = logger.CreateLogger<BaseController>();
            userId = commonHelper.GetUserId(HttpContext);
            SetThemes();
        }

        private void SetThemes()
        {
            string objPreferences = "";
            var themePreferences = new ThemePreferences() { ItemsPerPage = 10, Theme = "Default" };
            var cookieData = commonHelper.GetThemePreference(HttpContext);
             if (cookieData != null)
                objPreferences = cookieData;
            
            if (!string.IsNullOrEmpty(objPreferences))
                themePreferences = JsonConvert.DeserializeObject<ThemePreferences>(objPreferences);
 
            var newHref = "/Content/bootswatch/" + themePreferences.Theme + "/bootstrap.min.css";
            if (string.IsNullOrEmpty(themePreferences.Theme) || themePreferences.Theme == "Default")
                newHref = "/Content/bootstrap.min.css";
            ThemeBootstrap = newHref;
            ItemsPerPage = themePreferences.ItemsPerPage;
            ViewBag.ThemeBootstrap = newHref;
        }
    }
}