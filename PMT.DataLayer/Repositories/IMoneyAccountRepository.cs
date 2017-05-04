using System.Collections.Generic;
using PMT.Entities;
using PMT.Contracts.Repositories;
using PMT.Common;

namespace PMT.DataLayer.Repositories
{
    public interface IMoneyAccountRepository : IRepositoryBase<MoneyAccount>
    {
        List<MoneyAccount> GetMoneyAccounts(string userId);
        MoneyAccount GetMoneyAccount(string userId, int moneyAccountId);
        IActionStatus AddNewAccountWithInitialBalance(MoneyAccount moneyAccount, Transaction transaction);
        IActionStatus EditAccountNameAdjustBalance(MoneyAccount moneyAccount, Transaction transaction);
    }
}