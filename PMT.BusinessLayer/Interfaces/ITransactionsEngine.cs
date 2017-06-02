using PMT.Common;
using PMT.Common.Helpers;
using PMT.Entities;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface ITransactionsEngine
    {
        TransactionFilterVM GetFilter(string userId, string objPreferences);
        TransactionsSummaryVM PrepareSummary(string userId, TransactionFilterVM transactionFilterVM);

        ActionStatus InsertNewTransaction(Transaction transaction);
    }
}