using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.EventLog;
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
            var personalMoneyTrackerString = "PersonalMoneyTracker";

            var eventLongSettings = new EventLogSettings() {
                SourceName = personalMoneyTrackerString,
                LogName= personalMoneyTrackerString
            };


            var azureDiagnosticsSettings = new AzureAppServicesDiagnosticsSettings();
            azureDiagnosticsSettings.BlobName = personalMoneyTrackerString + "Log.txt";
            

            IUnityContainer container = UnityConfig.GetConfiguredContainer().Resolve<IUnityContainer>();
            var loggerFactory = container.Resolve<ILoggerFactory>();
            loggerFactory
                .AddConsole()
                .AddDebug()
                .AddEventLog(eventLongSettings)
                .AddAzureWebAppDiagnostics();

            return loggerFactory;
        }
    }
}