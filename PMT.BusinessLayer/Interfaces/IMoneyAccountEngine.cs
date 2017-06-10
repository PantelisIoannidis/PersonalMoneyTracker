using System.Collections.Generic;
using PMT.Common;
using PMT.Entities;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        void AddNewAccountWithInitialBalance(MoneyAccountVM moneyAccountVM);
        void EditAccountNameAdjustBalance(MoneyAccountVM moneyAccountVM);
        List<MoneyAccount> GetMoneyAccounts(string userId);
        List<MoneyAccountVM> GetMoneyAccountsWithBalance(string userId);
        List<MoneyAccount> GetMoneyAccountsPlusAll(string userId);
    }
}