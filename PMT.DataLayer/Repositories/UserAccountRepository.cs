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
    }
}
