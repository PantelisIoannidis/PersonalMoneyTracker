using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.Common.Resources;
using PMT.Entities;
using PMT.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Controllers
{
    public class SettingsController : Controller
    {

        ILogger logger;
        ICommonHelper commonHelper;
        IUserSettingsEngine userSettingsEngine;
        private string userId;

        public SettingsController(ILoggerFactory logger,
                                ICommonHelper commonHelper,
                                IUserSettingsEngine userSettingsEngine) 
        {
            this.logger = logger.CreateLogger<SettingsController>();
            this.commonHelper = commonHelper;
            this.userSettingsEngine = userSettingsEngine;
            userId = commonHelper.GetUserId(HttpContext);
        }


        public ActionResult Index()
        {
            var settings = userSettingsEngine.GetUserSettings(userId);
            if (settings == null)
            {
                settings = new UserSettings() { UserId = userId, ItemsPerPage = 10,Theme="Default" };
            }
            var dict = userSettingsEngine.GetThemes();
            SelectList SelectList = new SelectList((IEnumerable)dict, "Key", "Value", settings.Theme);
            ViewBag.Theme = SelectList;
            
            return View(settings);
        }

        [HttpPost]
        public ActionResult SaveSettings(UserSettings userSettings, string preferences)
        {
            if (ModelState.IsValid)
            {
                TempData[GlobalCookies.themePreferenceCookie] = preferences;
                commonHelper.SetThemePreference(HttpContext, preferences);
                var result = userSettingsEngine.StoreUserSettings(userSettings);
                if (result.Status)
                {
                    TempData[Notifications.NotificationSuccess] = MessagesText.SettingsUpdated;
                    return RedirectToAction(nameof(SettingsController.Index));
                }
            }
            TempData[Notifications.NotificationWarning] = MessagesText.SettingsCouldntBeUpdated;
            return RedirectToAction(nameof(SettingsController.Index));
        }
    }
}