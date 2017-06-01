using Microsoft.Owin;
using Owin;
using PMT.Web.Helpers;

[assembly: OwinStartupAttribute(typeof(PMT.Web.Startup))]
namespace PMT.Web
{
    public partial class Startup
    {
        public Startup()
        {
            new LoggingHelper();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
