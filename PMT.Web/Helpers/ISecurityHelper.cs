using System.Web;

namespace PMT.Web.Helpers
{
    public interface ISecurityHelper
    {
        string GetUserId(HttpContextBase httpContext);
    }
}