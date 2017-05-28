using PMT.Entities;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface IChartsEngine
    {
        string ChartIncomeVsExpense(string userId, TransactionFilterVM transactionFilterVM);

        string ChartIncomeExpensesByCategory(string userId, TransactionFilterVM transactionFilterVM, TransactionType transactionType);
    }
}