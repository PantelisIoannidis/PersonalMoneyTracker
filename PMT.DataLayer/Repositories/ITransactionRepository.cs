using PMT.Entities;
using PMT.Contracts.Repositories;
using PMT.Common.Helpers;
using System.Collections.Generic;
using System.Linq;
using PMT.Models;

namespace PMT.DataLayer.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        IQueryable<TransactionVM> GetTransactionsVM(string userId, Period timeDuration);

        IQueryable<TransactionVM> GetTransactionsVM(string userId, Period timeDuration, int account);

        TransactionVM GetTransactionVM(int transactionId);

        decimal GetAdjustment(string userId);
        decimal GetAdjustmentPerAccount(string userId, int moneyAccountId);
        decimal GetBalance(string userId);
        decimal GetBalancePerAccount(string userId, int moneyAccountId);
        decimal GetExpense(string userId);
        decimal GetExpensePerAccount(string userId, int moneyAccountId);
        decimal GetIncome(string userId);
        decimal GetIncomePerAccount(string userId, int moneyAccountId);
    }
}