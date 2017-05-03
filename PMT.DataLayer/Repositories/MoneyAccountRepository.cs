using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class MoneyAccountRepository : RepositoryBase<MoneyAccount>, IMoneyAccountRepository
    {
       
        public MoneyAccountRepository()
            :base(new MainDb())
        {
           
        }

        public List<MoneyAccount> GetMoneyAccounts(string userId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId).ToList();
        }
        public MoneyAccount GetMoneyAccount(string userId,int moneyAccountId)
        {
            return db.MoneyAccounts.Where(u => u.UserId == userId && u.MoneyAccountId==moneyAccountId).FirstOrDefault();
        }
    }
}
