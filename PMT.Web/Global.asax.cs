using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PMT.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var loggerFactory = new LoggingHelper().GetLogger();

            Exception exception = Server.GetLastError();
            loggerFactory.CreateLogger<MvcApplication>().LogError(LoggingEvents.UNKNOWN, exception, "logged at global level");
            Server.ClearError();
            Response.Redirect("/Errors/Error");
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("lang"))
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
            return base.GetVaryByCustomString(context, custom);
        }
    }
}
    