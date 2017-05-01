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

namespace PMT.Web.Controllers
{
    [Authorize]
    public class UserAccountsController : Controller
    {
        IUserAccountRepository userAccountRepository;
        ISecurityHelper securityHelper;
        IUserAccountEngine userAccountEngine;
        public UserAccountsController(ISecurityHelper securityHelper, 
                                        IUserAccountRepository userAccountRepository,
                                        IUserAccountEngine userAccountEngine)
        {
            this.userAccountRepository = userAccountRepository;
            this.securityHelper = securityHelper;
            this.userAccountEngine = userAccountEngine;
        }

        // GET: UserAccounts
        public ActionResult Index()
        {
            var userId = securityHelper.GetUserId(this.HttpContext);
            var userAccounts = userAccountRepository.GetAccounts(userId);
            return View( userAccounts );
        }

        //GET: UserAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = userAccountRepository.GetById(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {

            var userId = securityHelper.GetUserId(HttpContext);
            UserAccountCreateNewMV userAccount = new UserAccount() { UserId = userId } as UserAccountCreateNewMV;
            return View(userAccount);
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,InitialAmount")] UserAccountCreateNewMV userAccount)
        {
            if (ModelState.IsValid)
            {
                userAccount.UserId = securityHelper.GetUserId(HttpContext);
                userAccountEngine.AddNewAccountWithInitialBalance(userAccount);
                return RedirectToAction("Index");
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = userAccountRepository.GetById(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserAccountId,UserId,Name")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                userAccountRepository.Update(userAccount);
                userAccountRepository.Save();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = userAccountRepository.GetById(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccount userAccount = userAccountRepository.GetById(id);
            userAccountRepository.Delete(id);
            userAccountRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userAccountRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
