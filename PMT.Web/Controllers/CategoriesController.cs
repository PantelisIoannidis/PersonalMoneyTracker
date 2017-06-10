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
using System.Net;
using System.Web;
using System.Web.Mvc;
using static PMT.Entities.Literals;

namespace PMT.Web.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
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
                                        ) : base(logger, commonHelper)
        {
            this.commonHelper = commonHelper;
            this.logger = logger.CreateLogger<CategoriesController>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.categoriesEngine = categoriesEngine;
            this.iconRepository = iconRepository;

        }

        [MoveNotificationsDataFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadIndexPanelPartial()
        {
            var cateogories = categoriesEngine.GetAllGategoriesSubCategories(userId);
            return PartialView("_IndexPanelPartial", cateogories);
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
                IconId = DefaultCategoryValues.IconId,
                UserId=userId
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
                TempData["NotificationSuccess"] = "New category has been created";
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewCategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            ViewBag.NotificationWarning = "New category couldn't be created";
            return View(categoryVM);
        }

        public ActionResult NewSubCategory()
        {
            var categoryVM = new CategoryVM()
            {
                IconId = DefaultCategoryValues.IconId,
                UserId=userId
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
                TempData["NotificationSuccess"] = "New subcategory has been created";
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewSubcategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            ViewBag.NotificationWarning = "New subcategory couldn't be created";
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var status = categoriesEngine.DeleteCategorySubCategories(userId,id);
            if(status.ExceptionFromConditions)
            {
                logger.LogError(LoggingEvents.GET_ITEM_NOTFOUND, "Delete not completed. Id not found");
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return Json(new { message= "Successfully deleted" });
            else
                return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var categoryVM = categoriesEngine.GetCategory(userId,id);
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
                if (categoryVM.IsCategory)
                    TempData["NotificationSuccess"] = "Category has been modified";
                else
                    TempData["NotificationSuccess"] = "Subcategory has been modified";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryType = GetShortTransactionTypeList();
            if (categoryVM.IsCategory)
                ViewBag.NotificationWarning = "Category couldn't be modified";
            else
                ViewBag.NotificationWarning = "Subcategory couldn't be modified";
            return View(categoryVM);
        }
    }
}