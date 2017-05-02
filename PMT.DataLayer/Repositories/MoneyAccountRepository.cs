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

        public List<MoneyAccount> GetAccounts(string userId)
        {
            return context.MoneyAccounts.Where(u => u.UserId == userId).ToList();
        }
    }
}
