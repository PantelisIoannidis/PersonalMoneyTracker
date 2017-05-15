using System.Linq;
using PMT.Common.Helpers;
using PMT.Entities;
using PMT.Models;
using PMT.Contracts.Repositories;

namespace PMT.DataLayer.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        decimal GetBalance(string userId, int moneyAccountId = -1, Period period = null);
        decimal GetBalance(string userId, int moneyAccountId, Period period, TransactionType transactionType);
        IQueryable<TransactionVM> GetTransactionsVM(string userId, Period period);
        IQueryable<TransactionVM> GetTransactionsVM(string userId, Period period, int account);
        TransactionVM GetTransactionVM(int transactionId);
        void Update(Transaction transaction);
    }
}