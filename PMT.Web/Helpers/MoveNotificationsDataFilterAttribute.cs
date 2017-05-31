using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Helpers
{
    public class MoveNotificationsDataFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var TempData = filterContext.Controller.TempData;
            var viewBag = filterContext.Controller.ViewBag;


            var tNotificationSuccess = (string)TempData["NotificationSuccess"];
            var tNotificationDanger = (string)TempData["NotificationDanger"];
            var tNotificationWarning = (string)TempData["NotificationWarning"];
            if (!string.IsNullOrEmpty(tNotificationSuccess))
                viewBag.NotificationSuccess = tNotificationSuccess;
            if (!string.IsNullOrEmpty(tNotificationDanger))
                viewBag.tNotificationDanger = tNotificationDanger;
            if (!string.IsNullOrEmpty(tNotificationWarning))
                viewBag.tNotificationWarning = tNotificationWarning;

            base.OnActionExecuting(filterContext);
        }
    }
}