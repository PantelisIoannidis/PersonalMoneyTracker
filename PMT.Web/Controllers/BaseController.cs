using PMT.Common;
using PMT.Entities;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMT.Web.Controllers
{
    public class BaseController : Controller
    {
        ICommonHelper commonHelper;
        public BaseController(ICommonHelper commonHelper)
        {
            this.commonHelper = commonHelper;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            commonHelper.ApplyServerCulture(HttpContext);
            return base.BeginExecuteCore(callback, state);
        }


        public string GetCulture()
        {
            return commonHelper.GetDisplayLanguage(HttpContext);
        }
    }
}