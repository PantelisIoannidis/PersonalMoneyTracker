using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using PMT.Web.App_Start;

[assembly: OwinStartupAttribute(typeof(PMT.Web.Startup))]
namespace PMT.Web
{
    public partial class Startup
    {
        public Startup()
        {
            IUnityContainer container = UnityConfig.GetConfiguredContainer().Resolve<IUnityContainer>();
            var loggerFactory = container.Resolve<ILoggerFactory>();
            loggerFactory.AddConsole().AddDebug();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
