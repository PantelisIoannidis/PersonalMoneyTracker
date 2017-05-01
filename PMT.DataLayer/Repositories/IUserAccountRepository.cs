using System.Collections.Generic;
using PMT.Entities;
using PMT.Contracts.Repositories;

namespace PMT.DataLayer.Repositories
{
    public interface IUserAccountRepository : IRepositoryBase<UserAccount>
    {
        List<UserAccount> GetAccounts(string userId);
    }
}