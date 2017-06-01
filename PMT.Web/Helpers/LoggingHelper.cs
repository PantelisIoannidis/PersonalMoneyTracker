using Microsoft.Extensions.Logging;
using Microsoft.Practices.Unity;
using PMT.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMT.Web.Helpers
{
    public class LoggingHelper
    {
        public ILoggerFactory GetLogger()
        {
            IUnityContainer container = UnityConfig.GetConfiguredContainer().Resolve<IUnityContainer>();
            var loggerFactory = container.Resolve<ILoggerFactory>();
            loggerFactory.AddConsole().AddDebug();

            return loggerFactory;
        }
    }
}