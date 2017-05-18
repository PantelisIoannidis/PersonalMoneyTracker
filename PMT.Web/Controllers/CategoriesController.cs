using Microsoft.Extensions.Logging;
using PMT.Contracts.Repositories;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMT.Web.Controllers
{
    public class CategoriesController : Controller
    {
        ILogger logger;
        ICommonHelper commonHelper;
        ICategoryRepository categoryRepository;
        public CategoriesController(ILoggerFactory logger,
                                        ICommonHelper commonHelper,
                                        ICategoryRepository categoryRepository
                                        )
        {
            this.commonHelper = commonHelper;
            this.categoryRepository = categoryRepository;
            this.logger = logger.CreateLogger<CategoriesController>();
        }

        public ActionResult Index()
        {
            var cateogories = categoryRepository.GetAllGategoriesSubCategories();
            return View(cateogories);
        }
    }
}