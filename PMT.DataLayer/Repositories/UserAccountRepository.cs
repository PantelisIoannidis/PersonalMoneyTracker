using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountRepository
    {
       
        public UserAccountRepository()
            :base(new MainDb())
        {
           
        }

        public List<UserAccount> GetAccounts(string userId)
        {
            return context.UserAccounts.Where(u => u.UserId == userId).ToList();
        }
    }
}
