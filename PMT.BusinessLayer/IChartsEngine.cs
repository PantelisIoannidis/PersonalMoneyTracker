using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface IChartsEngine
    {
        string ChartIncomeVsExpense(string userId, TransactionFilterVM transactionFilterVM);
    }
}