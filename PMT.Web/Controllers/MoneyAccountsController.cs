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
using PMT.DataLayer.Repositories;
using PMT.Web.Helpers;
using PMT.Common;
using PMT.Models;
using PMT.BusinessLayer;
using Microsoft.Extensions.Logging;

namespace PMT.Web.Controllers
{
    [Authorize]
    public class MoneyAccountsController : Controller
    {
        ILogger logger;
        IMoneyAccountRepository moneyAccountRepository;
        ICommonHelper securityHelper;
        IMoneyAccountEngine moneyAccountEngine;
        ITransactionRepository transactionRepository;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        public MoneyAccountsController(ICommonHelper securityHelper, 
                                        IMoneyAccountRepository moneyAccountRepository,
                                        IMoneyAccountEngine moneyAccountEngine,
                                        ITransactionRepository transactionRepository,
                                        ICategoryRepository categoryRepository,
                                        ISubCategoryRepository subCategoryRepository,
                                        ILoggerFactory logger)
        {
            this.moneyAccountRepository = moneyAccountRepository;
            this.securityHelper = securityHelper;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.moneyAccountEngine = moneyAccountEngine;
            this.transactionRepository = transactionRepository;
            this.logger = logger.CreateLogger<MoneyAccountsController>();
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            //var categories = categoryRepository.GetGategory(type);
            var categories = categoryRepository.GetAll();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [MoveNotificationsDataFilter]
        public ActionResult Index()
        {
            logger.LogInformation("MoneyAccountsController Index");
            var userId = securityHelper.GetUserId(this.HttpContext);
            ViewBag.TotalBalance = transactionRepository.GetBalance(userId);
            return View(moneyAccountEngine.GetMoneyAccountsWithBalance(userId));
        }

        // GET: MoneyAccounts/Create
        public ActionResult Create()
        {

            var userId = securityHelper.GetUserId(HttpContext);
            MoneyAccount moneyAccount = new MoneyAccount() { UserId = userId };
            return View(moneyAccount);
        }

        // POST: MoneyAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Balance")] MoneyAccount moneyAccount)
        {
            if (ModelState.IsValid)
            {
                moneyAccount.UserId = securityHelper.GetUserId(HttpContext);
                moneyAccountEngine.AddNewAccountWithInitialBalance(moneyAccount);
                TempData["NotificationSuccess"] = "New account has been created";
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = "New account couldn't be created";
            return View(moneyAccount);
        }

        // GET: MoneyAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyAccount moneyAccount = moneyAccountRepository.GetById(id);
            if (moneyAccount == null)
            {
                return HttpNotFound();
            }
            return View(moneyAccount);
        }

        // POST: MoneyAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MoneyAccountId,UserId,Name,Balance")] MoneyAccount moneyAccount)
        {
            if (ModelState.IsValid)
            {
                moneyAccountEngine.EditAccountNameAdjustBalance(moneyAccount);
                TempData["NotificationSuccess"] = "Account has been modified";
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = "Account couldn't be modified";
            return View(moneyAccount);
        }

        // GET: MoneyAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyAccount moneyAccount = moneyAccountRepository.GetById(id);
            if (moneyAccount == null)
            {
                return HttpNotFound();
            }
            return View(moneyAccount);
        }

        // POST: MoneyAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoneyAccount moneyAccount = moneyAccountRepository.GetById(id);
            moneyAccountRepository.Delete(id);
            moneyAccountRepository.Save();
            TempData["NotificationSuccess"] = "Account successfully deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                moneyAccountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
