﻿using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.DataLayer.Repositories;
using PMT.Models;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Controllers
{
    public class CategoriesController : Controller
    {
        ILogger logger;
        ICommonHelper commonHelper;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        ICategoriesEngine categoriesEngine;
        IIconRepository iconRepository;
        public CategoriesController(ILoggerFactory logger,
                                        ICommonHelper commonHelper,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        ICategoriesEngine categoriesEngine,
                                        IIconRepository iconRepository
                                        )
        {
            this.commonHelper = commonHelper;
            this.categoryRepository = categoryRepository;
            this.logger = logger.CreateLogger<CategoriesController>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.categoriesEngine = categoriesEngine;
            this.iconRepository = iconRepository;
        }

        public ActionResult Index()
        {
            var cateogories = categoryRepository.GetAllGategoriesSubCategories();
            return View(cateogories);
        }

        public JsonResult GetAllIcons()
        {
            var icons = iconRepository.GetAll().ToList();
            return Json(icons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewCategory()
        {
            var categoryVM = new CategoryVM();
            List<SelectListItem> types = new List<SelectListItem>() {
                new SelectListItem()
                {
                    Text = TransactionTypes.Income.TransactionTypeDescription(),
                    Value = TransactionTypes.Income.ToString()
                },
                new SelectListItem()
                {
                    Text = TransactionTypes.Expense.TransactionTypeDescription(),
                    Value = TransactionTypes.Expense.ToString()
                }
            };
            ViewBag.CategoryType = types;
            return View(categoryVM);
        }

        public ActionResult Modify(string id)
        {
            var categoryVM = categoriesEngine.GetCategory(id);
            return View(categoryVM);
        }
    }
}