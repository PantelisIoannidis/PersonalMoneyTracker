using PMT.Models;
using PMT.Common;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PMT.BusinessLayer
{
    public class MoneyAccountEngine : IMoneyAccountEngine
    {
        ILogger logger;
        IMoneyAccountRepository moneyAccountRepository;
        IActionStatus actionStatus;
        ITransactionRepository transactionRepository;
        public MoneyAccountEngine(ILoggerFactory logger,
                                IMoneyAccountRepository MoneyAccountRepository,
                                ITransactionRepository transactionRepository,
                                IActionStatus actionStatux)
        {
            this.moneyAccountRepository = MoneyAccountRepository;
            this.actionStatus = actionStatux;
            this.transactionRepository = transactionRepository;
            this.logger = logger.CreateLogger<MoneyAccountEngine>();
        }

        public void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount)
        {
           try
            {
                var transaction = new Transaction()
                {
                    UserId = moneyAccount.UserId,
                    MoneyAccountId = moneyAccount.MoneyAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = moneyAccount.Balance,
                    TransactionDate = DateTime.Now,
                    Description = ModelText.MoneyAccountName +" "+ ViewText.InitialBalance
                };
                
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "AddNewAccountWithInitialBalance didn't run");
            }
        }

        public List<MoneyAccount> GetMoneyAccountBalance(string userId)
        {
            var moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
            foreach(var moneyAccount in moneyAccounts)
                moneyAccount.Balance = transactionRepository.GetBalance(userId, moneyAccount.MoneyAccountId);
            return moneyAccounts;
        }

        public void EditAccountNameAdjustBalance(MoneyAccount moneyAccount)
        {
            try
            {
                var transaction = new Transaction()
                {
                    UserId = moneyAccount.UserId,
                    MoneyAccountId = moneyAccount.MoneyAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = moneyAccount.Balance,
                    TransactionDate = DateTime.Now,
                    Description = ModelText.MoneyAccountName + " " + ViewText.Adjustment
                };
                moneyAccountRepository.EditAccountNameAdjustBalance(moneyAccount, transaction);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "EditAccountNameAdjustBalance didn't run");
            }
        }
        public List<MoneyAccount> GetMoneyAccountsPlusAll(string userId)
        {
            var account = new MoneyAccount() {
                UserId=userId,
                MoneyAccountId=-1,
                Name=ViewText.AllAccounts
            };
            var dbmoneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
            var moneyAccounts = new List<MoneyAccount>();
            moneyAccounts.Add(account);
            moneyAccounts.AddRange(dbmoneyAccounts);
            return moneyAccounts;
        }
    }
}
