using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository()
            : base(new MainDb())
        {
        }

        public decimal GetBalance(string userId)
        {
            return db.Transactions
                    .Where(t=>t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }
        public decimal GetBalancePerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.UserId == userId && t.MoneyAccountId==moneyAccountId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetIncome(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Income && t.UserId==userId)
                    .Sum(s => (decimal?)s.Amount)??0;
        }

        public decimal GetAdjustment(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetExpense(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetIncomePerAccount(string userId,int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Income && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetAdjustmentPerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetExpensePerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

    }
}
