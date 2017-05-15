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
using PMT.Contracts.Repositories;
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
        ILogger logger;
        ICommonHelper commonHelper;
        ITransactionRepository transactionRepository;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        IMoneyAccountRepository moneyAccountRepository;
        IMoneyAccountEngine moneyAccountEngine;
        IUserPreferences userPreferences;
        IMapping mapping;

        public TransactionsController(ILoggerFactory logger,
                                        IUserPreferences userPreferences,
                                        ICommonHelper commonHelper,
                                        ITransactionRepository transactionRepository,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        IMoneyAccountRepository moneyAccountRepository,
                                        IMoneyAccountEngine moneyAccountEngine,
                                        IMapping mapping
                                        )
        {
            this.commonHelper = commonHelper;
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.moneyAccountRepository = moneyAccountRepository;
            this.moneyAccountEngine = moneyAccountEngine;
            this.mapping = mapping;
            this.userPreferences = userPreferences;
            this.logger = logger.CreateLogger<TransactionsController>();
        }

        public const string transactionPreferences = "transactionPreferences";

        public ActionResult Index(int? page=1)
        {
            DateTime selectedDate = DateTime.Now;
            Period period;

            var userId = commonHelper.GetUserId(HttpContext);
            var postsPerPage = 3;
            TransactionFilterVM transactionFilterVM = new TransactionFilterVM();
            string objPreferences = TempData[transactionPreferences] as string;
            if(string.IsNullOrEmpty(objPreferences))
                objPreferences = userPreferences.GetTransactionPreferences(HttpContext);
            if (!string.IsNullOrEmpty(objPreferences))
            {
                var pref = JsonConvert.DeserializeObject<TransactionsFilterPreferences>(objPreferences);
                period = new Period(DateTime.Parse(pref.SelectedDateFull),(PeriodType)pref.PeriodFilterId);
                if (pref.MoveToNextFlag >0)
                    period.MoveToNext();
                if (pref.MoveToNextFlag <0)
                    period.MoveToPrevious();
                transactionFilterVM.PeriodFilterId = pref.PeriodFilterId;
                transactionFilterVM.AccountFilterId = pref.AccountFilterId;
                transactionFilterVM.SelectedDateFull = period.SelectedDate.ToString("s");
                transactionFilterVM.PeriodDescription = period.GetDescription();
                selectedDate = period.SelectedDate;
            } else
            {
                transactionFilterVM.AccountFilterId = -1;
                transactionFilterVM.PeriodFilterId = (int)PeriodType.Week;
                transactionFilterVM.SelectedDateFull = selectedDate.ToString("s");
                period = new Period(DateTime.Parse(transactionFilterVM.SelectedDateFull), PeriodType.Week);
                transactionFilterVM.PeriodDescription = period.GetDescription();
            }
                
            transactionFilterVM.PeriodEnum = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToDictionary(e => (int)e, e => e.ToString());
            transactionFilterVM.MoneyAccountChoiceFilter = moneyAccountEngine.GetMoneyAccountsPlusAll(userId);

            

            var transactionsVM = transactionRepository.GetTransactionsVM(userId, period, transactionFilterVM.AccountFilterId);
            var cnt = transactionsVM.Count();
            var pager = new Pager(cnt, page.Value, postsPerPage);
            var aPage = transactionsVM
                .Skip(pager.Skip)
                .Take(postsPerPage);
                
            ViewBag.Pager = pager;
            var list = aPage.ToList();
            var tuple = new Tuple<IEnumerable<TransactionVM>, TransactionFilterVM>(list, transactionFilterVM);
            return View(tuple);
        }

        public void SetUserPreferences(string preferences)
        {
            TempData[transactionPreferences] = preferences;
            userPreferences.SetTransactionPreferences(HttpContext, preferences);
        }

        public ActionResult GetAccountsAvailableForTransfer(int accountId)
        {
            var userId = commonHelper.GetUserId(HttpContext);
            var accounts = moneyAccountRepository.GetMoneyAccountsExcludingCurrent(userId,accountId);
            
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategories(TransactionType type)
        {
            var categories = categoryRepository.GetGategory(type);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategories(int categoryId)
        {
            var subCategories = subCategoryRepository.GetSubCategory(categoryId);
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var userId = commonHelper.GetUserId(HttpContext);
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
            var userId = commonHelper.GetUserId(HttpContext);
            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                transactionRepository.Insert(transaction);
                transactionRepository.Save();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = commonHelper.GetUserId(HttpContext);
            var moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
            ViewBag.MoneyAccountId = new SelectList(moneyAccounts, "MoneyAccountId", "Name", moneyAccounts.FirstOrDefault());
            var transaction = transactionRepository.GetById(id);
            
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,UserId,MoneyAccountId,CategoryId,SubCategoryId,TransactionType,TransactionDate,Description,Amount,MoveToAccount")] Transaction transaction)
        {
            var userId = commonHelper.GetUserId(HttpContext);
            transaction.UserId = userId;
            if (ModelState.IsValid)
            {
                transactionRepository.Update(transaction);
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Delete/5
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

        // POST: Transactions/Delete/5
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
