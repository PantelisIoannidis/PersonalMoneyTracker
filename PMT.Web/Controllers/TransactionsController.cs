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
        IMapping mapping;

        public TransactionsController(ILoggerFactory logger,
                                        ICommonHelper commonHelper,
                                        ITransactionRepository transactionRepository,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        IMoneyAccountRepository moneyAccountRepository,
                                        IMapping mapping
                                        )
        {
            this.commonHelper = commonHelper;
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.moneyAccountRepository = moneyAccountRepository;
            this.mapping = mapping;
            this.logger = logger.CreateLogger<TransactionsController>();
        }

        // GET: Transactions
        public ActionResult Index()
        {
            
            var userId = commonHelper.GetUserId(HttpContext);
            var transactionsVM = transactionRepository.GetTransactionsVM(userId, new Common.Helpers.TimeDuration(DateTime.UtcNow));
            return View(transactionsVM.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            return null;
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

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
