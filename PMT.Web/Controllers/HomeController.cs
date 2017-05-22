using Microsoft.Extensions.Logging;
using PMT.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ILogger logger;
        public HomeController(ILoggerFactory logger)
        {
            this.logger = logger.CreateLogger<HomeController>();

        }
        public ActionResult Index()
        {
            logger.LogInformation("HomeController Index");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}