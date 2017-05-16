using Microsoft.Extensions.Logging;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    public class CommonController : Controller
    {
        public string userId;

        public ICommonHelper commonHelper;
        public CommonController(ICommonHelper commonHelper)
                                
        {
            this.commonHelper = commonHelper;
            InitCommonVariables();
        }

        private void InitCommonVariables()
        {
            userId = commonHelper.GetUserId(HttpContext);
        }


    }
}