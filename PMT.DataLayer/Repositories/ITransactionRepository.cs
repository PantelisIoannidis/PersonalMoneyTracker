using PMT.Entities;
using PMT.Contracts.Repositories;

namespace PMT.DataLayer.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
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