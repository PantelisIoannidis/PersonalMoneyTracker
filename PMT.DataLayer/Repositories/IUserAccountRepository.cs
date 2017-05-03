using System.Collections.Generic;
using PMT.Entities;
using PMT.Contracts.Repositories;

namespace PMT.DataLayer.Repositories
{
    public interface IMoneyAccountRepository : IRepositoryBase<MoneyAccount>
    {
        List<MoneyAccount> GetMoneyAccounts(string userId);
        MoneyAccount GetMoneyAccount(string userId, int moneyAccountId);
    }
}