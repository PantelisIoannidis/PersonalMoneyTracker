using System.Collections.Generic;
using PMT.Entities;
using PMT.Common;
using PMT.Models;

namespace PMT.DataLayer.Repositories
{
    public interface IMoneyAccountRepository : IRepositoryBase<MoneyAccount>
    {
        IEnumerable<MoneyAccount> GetMoneyAccounts(string userId);
        MoneyAccount GetMoneyAccount(string userId, int moneyAccountId);
        List<MoneyAccount> GetMoneyAccountsExcludingCurrent(string userId, int moneyAccountId);
        List<MoneyAccountVM> GetMoneyAccountsWithBalance(string userId);
        MoneyAccount GetMoneyAccountwithBalance(string userId, int moneyAccountId);
        void AddNewAccountWithInitialBalance(MoneyAccount moneyAccount, Transaction transaction);
        void EditAccountNameAdjustBalance(MoneyAccount moneyAccount, Transaction transaction);

        IActionStatus DeleteAccount(int id);
    }
}