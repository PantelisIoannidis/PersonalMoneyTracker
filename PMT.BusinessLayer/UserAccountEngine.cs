using PMT.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class UserAccountEngine
    {
        IUserAccountRepository userAccountRepository;
        public UserAccountEngine(IUserAccountRepository userAccountRepository)
        {
            this.userAccountRepository = userAccountRepository;
        }

        public void AddNewAccountWithInitialBalance()
        {

        } 
    }
}
