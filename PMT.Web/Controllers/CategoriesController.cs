using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.Contracts.Repositories;
using PMT.Models;
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
        ISubCategoryRepository subCategoryRepository;
        ICategoriesEngine categoriesEngine;
        public CategoriesController(ILoggerFactory logger,
                                        ICommonHelper commonHelper,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        ICategoriesEngine categoriesEngine
                                        )
        {
            this.commonHelper = commonHelper;
            this.categoryRepository = categoryRepository;
            this.logger = logger.CreateLogger<CategoriesController>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.categoriesEngine = categoriesEngine;
        }

        public ActionResult Index()
        {
            var cateogories = categoryRepository.GetAllGategoriesSubCategories();
            return View(cateogories);
        }

        public ActionResult Create()
        {
            var cateogories = categoryRepository.GetAllGategoriesSubCategories();
            return View(cateogories);
        }

        public ActionResult Modify(string id)
        {
            var categoryVM = categoriesEngine.GetCategory(id);
            return View(categoryVM);
        }
    }
}