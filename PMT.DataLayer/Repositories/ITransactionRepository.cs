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
        IQueryable<TransactionVM> GetTransactions(string userId, TimeDuration timeDuration);

        IQueryable<TransactionVM> GetTransactions(string userId, TimeDuration timeDuration, int account);

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