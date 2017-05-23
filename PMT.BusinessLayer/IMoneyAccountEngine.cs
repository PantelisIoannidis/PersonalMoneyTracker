using System.Collections.Generic;
using PMT.Common;
using PMT.Entities;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount);
        void EditAccountNameAdjustBalance(MoneyAccount moneyAccount);
        List<MoneyAccount> GetMoneyAccountBalance(string userId);

        List<MoneyAccount> GetMoneyAccountsPlusAll(string userId);
    }
}