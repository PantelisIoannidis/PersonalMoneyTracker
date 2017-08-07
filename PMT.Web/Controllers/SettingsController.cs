using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.Common.Resources;
using PMT.Entities;
using PMT.Models;
using PMT.Web.Helpers;
using PMT.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SettingsController : BaseController
    {

        ILogger logger;
        ICommonHelper commonHelper;
        IUserSettingsEngine userSettingsEngine;
        private string userId;

        public SettingsController(ILoggerFactory logger,
                                ICommonHelper commonHelper,
                                IUserSettingsEngine userSettingsEngine) : base(commonHelper)
        {
            this.logger = logger.CreateLogger<SettingsController>();
            this.commonHelper = commonHelper;
            this.userSettingsEngine = userSettingsEngine;
            userId = commonHelper.GetUserId(HttpContext);
        }


        public ActionResult Index()
        {


            var currentClientLanguage = commonHelper.GetLanguageFormatting(HttpContext);
            var currentImplementedLanguage = commonHelper.GetDisplayLanguage(HttpContext);

            var settings = (UserSettings)userSettingsEngine.GetUserSettings(userId);
            if (settings == null)
            {
                settings = new UserSettings() {
                    UserId = userId,
                    ItemsPerPage = 10,
                    Theme = "Default",
                    DisplayLanguage = currentClientLanguage,
                    LanguageFormatting = currentImplementedLanguage
                };
            }

            ViewBag.DisplayLanguage = new SelectList(CultureHelper.GetImplementedDisplayLanguages(), "Key", "Value", currentImplementedLanguage);
            ViewBag.LanguageFormatting = new SelectList(commonHelper.GetClientCultureInfo(HttpContext), "Name", "NativeName", currentClientLanguage);
            ViewBag.Theme = new SelectList(userSettingsEngine.GetThemes(), "Key", "Value", settings.Theme); 

            return View(settings);
        }

        [HttpPost]
        public ActionResult SaveSettings(UserSettings userSettings, string preferences, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var serializedSettings = JsonConvert.SerializeObject(userSettings);
                TempData[GlobalCookies.themePreferenceCookie] = serializedSettings;
                commonHelper.SetThemePreference(HttpContext, serializedSettings);
                commonHelper.SetServerCulture(HttpContext, userSettings.DisplayLanguage, userSettings.LanguageFormatting);
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