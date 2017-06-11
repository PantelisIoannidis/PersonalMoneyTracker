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
    public class MoneyAccountEngine : BaseEngine, IMoneyAccountEngine
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
                    Description = moneyAccount.Name + " " + ViewText.InitialBalance
                };
                moneyAccountRepository.AddNewAccountWithInitialBalance(moneyAccount, transaction);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository, add new account with balance");
            }
        }

        public List<MoneyAccount> GetMoneyAccounts(string userId)
        {
            try
            {
                List<MoneyAccount> moneyAccounts = new List<MoneyAccount>();
                moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId).ToList();
                return moneyAccounts;
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository, Get MoneyAccounts List");
                return new List<MoneyAccount>();
            }
            
        }

        public List<MoneyAccountVM> GetMoneyAccountsWithBalance(string userId)
        {
            try
            {
                List<MoneyAccountVM> moneyAccounts = new List<MoneyAccountVM>();
                moneyAccounts = moneyAccountRepository.GetMoneyAccountsWithBalance(userId);
                return moneyAccounts;
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository, Get MoneyAccounts List");
                return new List<MoneyAccountVM>();
            }

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
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository, edit money account");
            }
        }
        public List<MoneyAccount> GetMoneyAccountsPlusAll(string userId)
        {
            var moneyAccounts = new List<MoneyAccount>();

            try
            {
                var account = new MoneyAccount()
                {
                    UserId = userId,
                    MoneyAccountId = -1,
                    Name = ViewText.AllAccounts
                };
                var dbmoneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
                moneyAccounts.Add(account);
                moneyAccounts.AddRange(dbmoneyAccounts);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository, Get MoneyAccounts and All option");
            }
            return moneyAccounts;
        }
    }
}
