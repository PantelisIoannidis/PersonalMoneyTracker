using PMT.Models;
using PMT.Common;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        IOperationStatus AddNewAccountWithInitialBalance(MoneyAccountCreateNewMV MoneyAccountCreateNewModelView);
    }
}