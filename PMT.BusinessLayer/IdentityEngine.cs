using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class IdentityEngine : IIdentityEngine
    {
        IIdentityRepository identityRepository;
        IUserAccountRepository userAccountRepository;
        ISeeding seeding;
        public IdentityEngine(IIdentityRepository identityRepository,
            IUserAccountRepository userAccountRepository,  ISeeding seeding)
        {
            this.identityRepository = identityRepository;
            this.userAccountRepository = userAccountRepository;
            this.seeding = seeding;
        }
        public string GetUserAccountId(string userName)
        {
            return identityRepository.GetUserId(userName);
        }

        public void InitializeNewUser(string userName)
        {
            var userAccount = seeding.GetDefaultAccountForNewUser(GetUserAccountId(userName));
            userAccountRepository.Insert(userAccount);
            userAccountRepository.Save();
        }
    }
}
