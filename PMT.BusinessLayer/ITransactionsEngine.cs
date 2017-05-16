using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface ITransactionsEngine
    {
        TransactionFilterVM GetFilter(string userId, string objPreferences);
    }
}