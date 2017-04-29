using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace PMT.Web.Helpers
{
    public class SecurityHelper : ISecurityHelper
    {
        public string GetUserId(HttpContextBase httpContext)
        {
           return HttpContext.Current.User.Identity.GetUserId(); 
        }
    }
}