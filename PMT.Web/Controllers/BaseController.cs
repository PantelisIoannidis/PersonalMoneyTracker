using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    public class BaseController : Controller
    {
        [ChildActionOnly]
        [MoveNotificationsDataFilter]
        public ActionResult ShowNotifications(string NotificationSuccess="",
                                            string NotificationDanger="",
                                            string NotificationWarning="")
        {
            return PartialView("_NotificationAreaPartial", new Tuple<string, string, string>(NotificationSuccess, NotificationDanger, NotificationWarning));
        }
    }
}