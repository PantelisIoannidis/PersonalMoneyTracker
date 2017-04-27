using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMT.Web.Startup))]
namespace PMT.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
