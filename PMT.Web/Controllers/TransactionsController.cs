using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMT.DataLayer;
using PMT.Entities;
using Microsoft.Extensions.Logging;
using PMT.Web.Helpers;
using PMT.DataLayer.Repositories;
using PMT.Models;
using PMT.Common;
using PMT.BusinessLayer;
using PMT.Common.Helpers;
using System.Collections;
using Newtonsoft.Json;

namespace PMT.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        public string userId;

        ILogger logger;
        IPeriod period;
        ICommonHelper commonHelper;
        ITransactionRepository transactionRepository;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        IMoneyAccountRepository moneyAccountRepository;
        IMoneyAccountEngine moneyAccountEngine;
        ITransactionsEngine transactionsEngine;
        IUserPreferences userPreferences;
        IMapping mapping;

        public TransactionsController(ILoggerFactory logger,
                                        IPeriod period,
                                        ICommonHelper commonHelper,
                                        IUserPreferences userPreferences,
                                         ITransactionRepository transactionRepository,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        IMoneyAccountRepository moneyAccountRepository,
                                        IMoneyAccountEngine moneyAccountEngine,
                                        ITransactionsEngine transactionsEngine,
                                        IMapping mapping
                                        )
        {
            
            this.transactionRepository = transactionRepository;
            this.transactionsEngine = transactionsEngine;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.moneyAccountRepository = moneyAccountRepository;
            this.moneyAccountEngine = moneyAccountEngine;
            this.mapping = mapping;
            this.commonHelper = commonHelper;
            this.userPreferences = userPreferences;
            this.period = period;
            this.logger = logger.CreateLogger<TransactionsController>();

            userId = commonHelper.GetUserId(HttpContext);
        }

        

        public const string transactionPreferences = "transactionPreferences";

        public ActionResult Index(int? page=1)
        {
            var tuple = PrepareTransactionVM(page);
            return View(tuple);
        }


        public Tuple<IEnumerable<TransactionVM>, TransactionFilterVM, PaginationVM> PrepareTransactionVM(int? page = 1)
        {
            DateTime selectedDate = DateTime.Now;

            var postsPerPage = 8;

            string objPreferences = TempData[transactionPreferences] as string;
            if (string.IsNullOrEmpty(objPreferences))
                objPreferences = userPreferences.GetTransactionPreferences(HttpContext);

            TransactionFilterVM transactionFilterVM = transactionsEngine.GetFilter(userId, objPreferences);
            period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);

            var transactionsVM = transactionRepository.GetTransactionsVM(userId, (Period)period, transactionFilterVM.AccountFilterId);
            var cnt = transactionsVM.Count();
            var pager = new Pager(cnt, page.Value, postsPerPage);

            PaginationVM pagination = new PaginationVM()
            {
                pager = pager,
                ControllerName = "Transactions",
                ActionName = "Index"
            };

            var aPage = transactionsVM
                .Skip(pager.Skip)
                .Take(postsPerPage);

            var list = aPage.ToList();

            ViewBag.Summary = transactionsEngine.PrepareSummary(userId, transactionFilterVM);

            var tuple = new Tuple<IEnumerable<TransactionVM>, TransactionFilterVM, PaginationVM>(list, transactionFilterVM, pagination);
            return tuple;
        }




        public void SetUserPreferences(string preferences)
        {
            TempData[transactionPreferences] = preferences;
            userPreferences.SetTransactionPreferences(HttpContext, preferences);
        }

        public ActionResult GetAccountsAvailableForTransfer(int accountId)
        {

            var accounts = moneyAccountRepository.GetMoneyAccountsExcludingCurrent(userId,accountId);
            
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategories(TransactionType type)
        {
            var categories = categoryRepository.GetGategories(type);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategories(int categoryId)
        {
            var subCategories = subCategoryRepository.GetSubCategories(categoryId);
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            var moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
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
        public ActionResult Create([Bind(Include = "TransactionId,UserId,MoneyAccountId,CategoryId,SubCategoryId,TransactionType,TransactionDate,Description,Amount,MoveToAccount")] Transaction transaction)
        {

            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                transactionRepository.Insert(transaction);
                transactionRepository.Save();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        public ActionResult Edit(int? id)
        {

            var moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
            ViewBag.MoneyAccountId = new SelectList(moneyAccounts, "MoneyAccountId", "Name", moneyAccounts.FirstOrDefault());
            var transaction = transactionRepository.GetById(id);
            
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,UserId,MoneyAccountId,CategoryId,SubCategoryId,TransactionType,TransactionDate,Description,Amount,MoveToAccount")] Transaction transaction)
        {

            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                transactionRepository.Update(transaction);
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transactionVM = transactionRepository.GetTransactionVM(id.Value);
            if (transactionVM == null)
                return HttpNotFound();
            return View(transactionVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transactionRepository.Delete(id);
            transactionRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                transactionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
