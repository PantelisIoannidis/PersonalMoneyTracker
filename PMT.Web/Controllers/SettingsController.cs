using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
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
    public class SettingsController : BaseController
    {

        ILogger logger;
        ICommonHelper commonHelper;
        IUserSettingsEngine userSettingsEngine;
        public SettingsController(ILoggerFactory logger,
                                ICommonHelper commonHelper,
                                IUserSettingsEngine userSettingsEngine) : base(logger, commonHelper)
        {
            this.logger = logger.CreateLogger<SettingsController>();
            this.commonHelper = commonHelper;
            this.userSettingsEngine = userSettingsEngine;
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
                commonHelper.SetThemePreference(HttpContext, preferences);
                var result = userSettingsEngine.StoreUserSettings(userSettings);
                if (result.Status)
                {
                    TempData["NotificationSuccess"] = "Settings updated";
                    return RedirectToAction(nameof(SettingsController.Index));
                }
            }
            TempData["NotificationWarning"] = "Settings couldn't be updated";
            return RedirectToAction(nameof(SettingsController.Index));
        }
    }
}