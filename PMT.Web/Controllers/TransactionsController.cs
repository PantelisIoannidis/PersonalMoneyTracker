using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMT.Entities;
using Microsoft.Extensions.Logging;
using PMT.Web.Helpers;
using PMT.Models;
using PMT.Common;
using PMT.BusinessLayer;
using PMT.Common.Helpers;
using System.Collections;
using Newtonsoft.Json;
using static PMT.Entities.Literals;
using PMT.Common.Resources;

namespace PMT.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    public class TransactionsController : BaseController
    {
        

        ILogger logger;
        IPeriod period;
        ICommonHelper commonHelper;
        ITransactionsEngine transactionsEngine;
        ICategoriesEngine categoriesEngine;
        IMoneyAccountEngine moneyAccountEngine;
        IMapping mapping;

        public TransactionsController(ILoggerFactory logger,
                                        IPeriod period,
                                        ICommonHelper commonHelper,
                                        ICategoriesEngine categoriesEngine,
                                        IMoneyAccountEngine moneyAccountEngine,
                                        ITransactionsEngine transactionsEngine,
                                        IMapping mapping
                                        ) : base(commonHelper)
        {

            this.categoriesEngine = categoriesEngine;
            this.transactionsEngine = transactionsEngine;
            this.moneyAccountEngine = moneyAccountEngine;
            this.mapping = mapping;
            this.commonHelper = commonHelper;
            this.period = period;
            this.logger = logger.CreateLogger<TransactionsController>();
            userId = commonHelper.GetUserId(HttpContext);

        }

        

        public const string transactionPreferences = "transactionPreferences";
        private string userId;

        [MoveNotificationsDataFilter]
        public ActionResult Index(int? page=1)
        {
            var tuple = PrepareTransactionVM(page);
            return View(tuple);
        }


        public Tuple<IEnumerable<TransactionVM>, TransactionFilterVM, PaginationVM> PrepareTransactionVM(int? page = 1)
        {
            DateTime selectedDate = DateTime.Now;

            int itemsPerPage = ViewBag.ItemsPerPage;

            string objPreferences = TempData[transactionPreferences] as string;
            if (string.IsNullOrEmpty(objPreferences))
                objPreferences = commonHelper.GetTransactionsPreferences(HttpContext);

            TransactionFilterVM transactionFilterVM = transactionsEngine.GetFilter(userId, objPreferences);
            period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);

            var transactionsVM = transactionsEngine.GetTransactionsVM(userId, (Period)period, transactionFilterVM.AccountFilterId);
            var cnt = transactionsVM.Count();
            var pager = new Pager(cnt, page.Value, itemsPerPage);

            PaginationVM pagination = new PaginationVM()
            {
                pager = pager,
                ControllerName = "Transactions",
                ActionName = "Index"
            };

            var aPage = transactionsVM
                .Skip(pager.Skip)
                .Take(itemsPerPage);

            var list = aPage.ToList();

            ViewBag.Summary = transactionsEngine.PrepareSummary(userId, transactionFilterVM);

            var tuple = new Tuple<IEnumerable<TransactionVM>, TransactionFilterVM, PaginationVM>(list, transactionFilterVM, pagination);
            return tuple;
        }




        public void SetUserPreferences(string preferences)
        {
            TempData[transactionPreferences] = preferences;
            commonHelper.SetTransactionsPreferences(HttpContext, preferences);
        }

        public ActionResult GetAccountsAvailableForTransfer(int accountId)
        {

            var accounts = moneyAccountEngine.GetMoneyAccountsExcludingCurrent(userId,accountId);
            
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategories(TransactionType type)
        {
            var categories = categoriesEngine.GetCategories(userId,type).Where(x=> string.IsNullOrEmpty(x.SpecialAttribute));
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategories(int categoryId)
        {
            var subCategories = categoriesEngine.GetSubCategories(userId,categoryId).Where(x => string.IsNullOrEmpty(x.SpecialAttribute));
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            var moneyAccounts = moneyAccountEngine.GetMoneyAccounts(userId);
            ViewBag.MoneyAccountId = new SelectList(moneyAccounts, "MoneyAccountId", "Name",moneyAccounts.FirstOrDefault());
            var transaction = new Transaction()
            {
                TransactionType = TransactionType.Expense,
                TransactionDate = DateTime.Now,
            };
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,UserId,MoneyAccountId,CategoryId,SubCategoryId,TransactionType,TransactionDate,Description,Amount,MoveToAccount,TransferTo")] Transaction transaction)
        {

            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                var result=transactionsEngine.InsertNewTransaction(transaction);
                if (!result.ExceptionFromConditions) { 
                    TempData[Notifications.NotificationSuccess] = MessagesText.NewTransactionHasBeenCreated;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.NotificationWarning = MessagesText.NewTransactionCouldntBeCreated;
            var moneyAccounts = moneyAccountEngine.GetMoneyAccounts(userId);
            ViewBag.MoneyAccountId = new SelectList(moneyAccounts, "MoneyAccountId", "Name", moneyAccounts.FirstOrDefault());
            return View(transaction);
        }

        public ActionResult Edit(int? id)
        {

            var moneyAccounts = moneyAccountEngine.GetMoneyAccounts(userId);
            ViewBag.MoneyAccountId = new SelectList(moneyAccounts, "MoneyAccountId", "Name", moneyAccounts.FirstOrDefault());
            var transaction = transactionsEngine.GetTransactionById(id??0);
            
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,UserId,MoneyAccountId,CategoryId,SubCategoryId,TransactionType,TransactionDate,Description,Amount,MoveToAccount")] Transaction transaction)
        {

            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                transactionsEngine.UpdateTransaction(transaction);
                TempData[Notifications.NotificationSuccess] = MessagesText.TransactionHasBeenModified;
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = MessagesText.TransactionCouldntBeModified;
            return View(transaction);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transactionVM = transactionsEngine.GetTransactionVM(id.Value);
            if (transactionVM == null)
                return HttpNotFound();
            return View(transactionVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transactionsEngine.DeleteTransaction(id);
            TempData[Notifications.NotificationSuccess] = MessagesText.TransactionSuccessfullyDeleted;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}
