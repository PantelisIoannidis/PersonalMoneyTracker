using System.Collections.Generic;
using PMT.Common;
using PMT.Entities;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        IActionStatus AddNewAccountWithInitialBalance(MoneyAccount moneyAccount);
        IActionStatus EditAccountNameAdjustBalance(MoneyAccount moneyAccount);
        List<MoneyAccount> GetMoneyAccountBalance(string userId);
    }
}