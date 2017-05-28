using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Common.Resources;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class MoneyAccountRepository : RepositoryBase<MoneyAccount>, IMoneyAccountRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        public MoneyAccountRepository(ILoggerFactory logger,IActionStatus actionStatus)
            :base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<MoneyAccountRepository>();
        }

        public List<MoneyAccount> GetMoneyAccounts(string userId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId).ToList();
        }
        public MoneyAccount GetMoneyAccount(string userId,int moneyAccountId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId && u.MoneyAccountId==moneyAccountId).FirstOrDefault();
        }

        public List<MoneyAccount> GetMoneyAccountsExcludingCurrent(string userId, int moneyAccountId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId && u.MoneyAccountId != moneyAccountId).ToList();
        }

        public void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount, Transaction transaction)
        {
            using(var dbTransaction = db.Database.BeginTransaction()) { 
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
    }
}
