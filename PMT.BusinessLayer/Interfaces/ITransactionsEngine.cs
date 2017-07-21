using PMT.Common;
using PMT.Common.Helpers;
using PMT.Entities;
using PMT.Models;
using System.Linq;

namespace PMT.BusinessLayer
{
    public interface ITransactionsEngine
    {
        TransactionFilterVM GetFilter(string userId, string objPreferences, int timeZoneOffset=0);
        TransactionsSummaryVM PrepareSummary(string userId, TransactionFilterVM transactionFilterVM);
        decimal GetBalance(string userId);
        ActionStatus InsertNewTransaction(Transaction transaction);
        TransactionVM GetTransactionVM(int transactionId);
        void UpdateTransaction(Transaction transaction);
        Transaction GetTransactionById(int id);
        void DeleteTransaction(int id);
        IQueryable<TransactionVM> GetTransactionsVM(string userId, Period period, int account);
    }
}