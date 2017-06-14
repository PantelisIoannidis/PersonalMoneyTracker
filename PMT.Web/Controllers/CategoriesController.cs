using Microsoft.Extensions.Logging;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.Common.Resources;
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
    public class CategoriesController : Controller
    {

        ILogger logger;
        ICommonHelper commonHelper;
        ICategoriesEngine categoriesEngine;
        IIconsEngine iconsEngine;
        private string userId;

        public CategoriesController(ILoggerFactory logger,
                                        ICommonHelper commonHelper,
                                        ICategoriesEngine categoriesEngine,
                                        IIconsEngine iconsEngine
                                        ) 
        {
            this.commonHelper = commonHelper;
            this.logger = logger.CreateLogger<CategoriesController>();
            this.categoriesEngine = categoriesEngine;
            this.iconsEngine = iconsEngine;
            userId = commonHelper.GetUserId(HttpContext);
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
            var icons = iconsEngine.GetAll().ToList();
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
                TempData[Notifications.NotificationSuccess] = MessagesText.NewCategoryHasBeenCreated;
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewCategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            ViewBag.NotificationWarning = MessagesText.NewCategoryCouldntBeCreated;
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
                TempData[Notifications.NotificationSuccess] = MessagesText.NewSubcategoryHasBeenCreated;
                return RedirectToAction("Index");
            }
            ViewBag.Title = ViewText.CreateNewSubcategory;
            ViewBag.CategoryType = GetShortTransactionTypeList();
            ViewBag.NotificationWarning = MessagesText.NewSubcategoryCouldntBeCreated;
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
                return Json(new { message= MessagesText.SuccessfullyDeleted });
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
                    TempData[Notifications.NotificationSuccess] = "Category has been modified";
                else
                    TempData[Notifications.NotificationSuccess] = "Subcategory has been modified";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryType = GetShortTransactionTypeList();
            if (categoryVM.IsCategory)
                ViewBag.NotificationWarning = MessagesText.CategoryHasBeenModified;
            else
                ViewBag.NotificationWarning = MessagesText.SubcategoryCouldntBeModified;
            return View(categoryVM);
        }
    }
}