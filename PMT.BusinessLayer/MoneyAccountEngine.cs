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

namespace PMT.BusinessLayer
{
    public class MoneyAccountEngine : IMoneyAccountEngine
    {
        IMoneyAccountRepository moneyAccountRepository;
        IActionStatus actionStatus;
        ITransactionRepository transactionRepository;
        public MoneyAccountEngine(IMoneyAccountRepository MoneyAccountRepository,
                                ITransactionRepository transactionRepository,
                                IActionStatus operationStatus)
        {
            this.moneyAccountRepository = MoneyAccountRepository;
            this.actionStatus = operationStatus;
            this.transactionRepository = transactionRepository;
        }

        public IActionStatus AddNewAccountWithInitialBalance(MoneyAccount moneyAccount)
        {
           try
            {
                moneyAccountRepository.Insert(moneyAccount);
                moneyAccountRepository.Save();
                var transaction = new Transaction()
                {
                    UserId = moneyAccount.UserId,
                    MoneyAccountId = moneyAccount.MoneyAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = moneyAccount.Balance,
                    TransactionDate = DateTime.UtcNow,
                    Description = ViewText.InitialBalance
                };
                transactionRepository.Insert(transaction);
                transactionRepository.Save();
            }
            catch(Exception ex)
            {
                actionStatus = ActionStatus.CreateFromException("", ex);
            }

            return actionStatus;
        }

        public List<MoneyAccount> GetMoneyAccountBalance(string userId)
        {
            var moneyAccounts = moneyAccountRepository.GetMoneyAccounts(userId);
            foreach(var moneyAccount in moneyAccounts)
                moneyAccount.Balance = transactionRepository.GetBalancePerAccount(userId, moneyAccount.MoneyAccountId);
            return moneyAccounts;
        }

        public IActionStatus EditAccountNameAdjustBalance(MoneyAccount moneyAccount)
        {
            try
            {
                moneyAccountRepository.Update(moneyAccount);
                moneyAccountRepository.Save();
                var transaction = new Transaction()
                {
                    UserId = moneyAccount.UserId,
                    MoneyAccountId = moneyAccount.MoneyAccountId,
                    TransactionType = TransactionType.Adjustment,
                    Amount = moneyAccount.Balance,
                    TransactionDate = DateTime.UtcNow,
                    Description = ViewText.Adjustment
                };
                transactionRepository.Insert(transaction);
                transactionRepository.Save();
            }
            catch (Exception ex)
            {
                actionStatus = ActionStatus.CreateFromException("", ex);
            }

            return actionStatus;
        }
    }
}
