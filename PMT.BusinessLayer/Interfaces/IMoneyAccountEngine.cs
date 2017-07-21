using System.Collections.Generic;
using PMT.Common;
using PMT.Entities;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface IMoneyAccountEngine
    {
        void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount, int timeZoneOffset=0);
        void EditAccountNameAdjustBalance(MoneyAccount moneyAccount, int timeZoneOffset=0);
        List<MoneyAccount> GetMoneyAccounts(string userId);
        List<MoneyAccountVM> GetMoneyAccountsWithBalance(string userId);
        List<MoneyAccount> GetMoneyAccountsPlusAll(string userId);

        List<MoneyAccount> GetMoneyAccountsExcludingCurrent(string userId, int accountId);

        MoneyAccount GetById(int id);
        IActionStatus DeleteAccount(int id);
    }
}