using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMT.Entities;
using PMT.Web.Helpers;
using PMT.Common;
using PMT.Models;
using PMT.BusinessLayer;
using Microsoft.Extensions.Logging;
using static PMT.Entities.Literals;
using PMT.Common.Resources;

namespace PMT.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    public class MoneyAccountsController :BaseController
    {
        ILogger logger;
        IMoneyAccountEngine moneyAccountEngine;
        ICommonHelper commonHelper;
        ICategoriesEngine categoriesEngine;
        ITransactionsEngine transactionsEngine;
        private string userId;

        public MoneyAccountsController(ICommonHelper commonHelper,
                                        IMoneyAccountEngine moneyAccountEngine,
                                        ICategoriesEngine categoriesEngine,
                                        ITransactionsEngine transactionsEngine,
                                        ILoggerFactory logger) : base(commonHelper)
        {
            this.commonHelper = commonHelper;
            this.categoriesEngine = categoriesEngine;
            this.moneyAccountEngine = moneyAccountEngine;
            this.logger = logger.CreateLogger<MoneyAccountsController>();
            this.transactionsEngine = transactionsEngine;
            userId = commonHelper.GetUserId(HttpContext);
        }


        [MoveNotificationsDataFilter]
        public ActionResult Index()
        {
            ViewBag.TotalBalance = transactionsEngine.GetBalance(userId);
            return View(moneyAccountEngine.GetMoneyAccountsWithBalance(userId));
        }

        // GET: MoneyAccounts/Create
        public ActionResult Create()
        {

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
                moneyAccount.UserId = userId;
                moneyAccountEngine.AddNewAccountWithInitialBalance(moneyAccount);
                TempData[Notifications.NotificationSuccess] = MessagesText.NewAccountHasBeenCreated;
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = MessagesText.NewAccountCouldntBeCreated;
            return View(moneyAccount);
        }

        // GET: MoneyAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyAccount moneyAccount = moneyAccountEngine.GetById(id??0);
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
                TempData[Notifications.NotificationSuccess] = MessagesText.AccountHasBeenModified;
                return RedirectToAction("Index");
            }
            ViewBag.NotificationWarning = MessagesText.AccountCouldntBeModified;
            return View(moneyAccount);
        }

        // GET: MoneyAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyAccount moneyAccount = moneyAccountEngine.GetById(id??0);
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
            moneyAccountEngine.DeleteAccount(id);
            TempData[Notifications.NotificationSuccess] = MessagesText.AccountSuccessfullyDeleted;
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
