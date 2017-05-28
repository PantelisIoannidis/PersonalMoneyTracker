using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
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

        private List<SelectListItem> GetShortTransactionTypeList()
        {
            return new List<SelectListItem>() {
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
        }

        public ActionResult NewCategory()
        {
            var categoryVM = new CategoryVM() {
                IconId = DefaultCategoryValues.IconId
            };
            ViewBag.Title = ViewText.CreateNewCategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCategory(CategoryVM categoryVM,string CategoryType)
        {
            if (ModelState.IsValid)
            {
                categoryVM.Type = (TransactionType)CategoryType.ParseInt();
                categoriesEngine.StoreNewCategoryAndSubCategory(categoryVM);
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewCategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            return View(categoryVM);
        }

        public ActionResult NewSubCategory()
        {
            var categoryVM = new CategoryVM()
            {
                IconId = DefaultCategoryValues.IconId
            };
            ViewBag.Title = ViewText.CreateNewSubcategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewSubCategory(CategoryVM categoryVM, string CategoryType)
        {
            if (ModelState.IsValid)
            {
                categoriesEngine.StoreNewSubCategory(categoryVM);
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewSubcategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            return View(categoryVM);
        }

        [HttpPost]
        public ActionResult Delete(string categoryId)
        {
            categoriesEngine.DeleteCategorySubCategories(categoryId);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            
            var categoryVM = categoriesEngine.GetCategory(id);
            ViewBag.CategoryType = GetShortTransactionTypeList();

            string _view = "";
            if (categoryVM.IsCategory)
            {
                 _view = "NewCategory";
                ViewBag.Title = ViewText.EditCategory;
            }
            else
            {
                _view = "NewSubCategory";
                ViewBag.Title = ViewText.EditSubcategory;
                
            }
                
            return View(_view, categoryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryVM categoryVM, string CategoryType)
        {
            if (ModelState.IsValid)
            {
                categoryVM.Type = (TransactionType)CategoryType.ParseInt();
                categoriesEngine.EditCategoryAndSubCategory(categoryVM);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryType = GetShortTransactionTypeList();
            return View(categoryVM);
        }
    }
}