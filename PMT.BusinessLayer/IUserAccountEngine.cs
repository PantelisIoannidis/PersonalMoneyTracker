using PMT.Models;
using PMT.Common;

namespace PMT.BusinessLayer
{
    public interface IUserAccountEngine
    {
        IOperationStatus AddNewAccountWithInitialBalance(UserAccountCreateNewMV userAccountCreateNewModelView);
    }
}