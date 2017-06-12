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

namespace PMT.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
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
                                        )
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

            var itemsPerPage = 8;

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
                    TempData["NotificationSuccess"] = "New transaction has been created";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.NotificationWarning = "New transaction couldn't be created";
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
                TempData["NotificationSuccess"] = "Transaction has been modified";
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = "Transaction couldn't be modified";
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
            TempData["NotificationSuccess"] = "Transaction successfully deleted";
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
