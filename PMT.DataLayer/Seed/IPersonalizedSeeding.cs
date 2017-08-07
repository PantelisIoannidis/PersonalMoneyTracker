using PMT.Entities;
using System.Collections.Generic;

namespace PMT.DataLayer.Seed
{
    public interface IPersonalizedSeeding
    {
        void Categories(string userId);
        MoneyAccount GetDefaultAccountForNewUser(string userId);

        List<Transaction> GetDemoData(string userId,int moneyAccountId);
    }
}