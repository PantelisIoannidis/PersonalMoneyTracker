using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Common.Resources;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class MoneyAccountRepository :  IMoneyAccountRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        MainDb db;
        public MoneyAccountRepository(MainDb db,ILoggerFactory logger, IActionStatus actionStatus)
        {
            this.db = db;
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<MoneyAccountRepository>();
        }

        public IEnumerable<MoneyAccount> GetMoneyAccounts(string userId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId);
        }
        public MoneyAccount GetMoneyAccount(string userId, int moneyAccountId)
        {
            return GetMoneyAccounts(userId).FirstOrDefault(x => x.MoneyAccountId == moneyAccountId && x.UserId == userId);
        }

        public List<MoneyAccountVM> GetMoneyAccountsWithBalance(string userId)
        {
            var moneyAccounts = (from m in db.MoneyAccounts
                                 where (m.UserId == userId)
                                 let income = (from t in db.Transactions
                                            where t.MoneyAccountId == m.MoneyAccountId &&
                                            t.TransactionType==TransactionType.Income &&
                                            t.UserId == userId
                                            select t).Sum(s => (decimal?)s.Amount) ?? 0
                                 let expense = (from t in db.Transactions
                                               where t.MoneyAccountId == m.MoneyAccountId &&
                                               t.TransactionType == TransactionType.Expense &&
                                               t.UserId == userId
                                               select t).Sum(s => (decimal?)s.Amount) ?? 0
                                 let adjustment = (from t in db.Transactions
                                                where t.MoneyAccountId == m.MoneyAccountId &&
                                                t.TransactionType == TransactionType.Adjustment &&
                                                t.UserId == userId
                                                select t).Sum(s => (decimal?)s.Amount) ?? 0
                                 select new MoneyAccountVM
                                 {
                                     MoneyAccountId = m.MoneyAccountId,
                                     UserId = m.UserId,
                                     Name = m.Name,
                                     Balance = income - expense + adjustment
                                 }).ToList();

            return moneyAccounts;
        }
        public MoneyAccount GetMoneyAccountwithBalance(string userId, int moneyAccountId)
        {
            return GetMoneyAccountsWithBalance(userId).FirstOrDefault(x => x.MoneyAccountId == moneyAccountId && x.UserId == userId);
        }

        public List<MoneyAccount> GetMoneyAccountsExcludingCurrent(string userId, int moneyAccountId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId && u.MoneyAccountId != moneyAccountId).ToList();
        }

        public void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount, Transaction transaction)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.MoneyAccounts.Add(moneyAccount);
                    db.SaveChanges();
                    transaction.MoneyAccountId = moneyAccount.MoneyAccountId;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    logger.LogError(LoggingEvents.INSERT_ITEM, ex, "Couldn't store new Account with initial balance");
                }
            }

        }

        public void EditAccountNameAdjustBalance(MoneyAccount moneyAccount, Transaction transaction)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var accountInDatabase = db.MoneyAccounts.SingleOrDefault(w => w.MoneyAccountId == moneyAccount.MoneyAccountId);
                    accountInDatabase.Name = moneyAccount.Name;
                    db.SaveChanges();
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    logger.LogError(LoggingEvents.UPDATE_ITEM, ex, "Couldn't update the Account and adjust balance");
                }
            }
        }

        public IActionStatus DeleteAccount(int id)
        {
            try
            {
                MoneyAccount moneyAccount = db.MoneyAccounts.FirstOrDefault(x => x.MoneyAccountId == id);
                if (moneyAccount != null)
                {
                    db.MoneyAccounts.Remove(moneyAccount);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Deleting MoneyAccount from database";
                actionStatus = ActionStatus.CreateFromException(errorMessage, ex);
                logger.LogError(LoggingEvents.DELETE_ITEM, ex, errorMessage);
            }
            return actionStatus;
        }

        public void Insert(MoneyAccount moneyaccount)
        {
            try
            {
                db.MoneyAccounts.Add(moneyaccount);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.INSERT_ITEM, ex, "Insert account in database");
            }
        }

        public MoneyAccount GetById(int id)
        {
            MoneyAccount account = new MoneyAccount();
            try
            {
                account = db.MoneyAccounts.FirstOrDefault(x => x.MoneyAccountId == id);
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, "Get Account from the database");
            }
            return account;
        }
    }
}
