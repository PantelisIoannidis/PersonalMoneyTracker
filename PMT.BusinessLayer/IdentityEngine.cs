using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seed;
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
        IMoneyAccountRepository MoneyAccountRepository;
        ISeedingLists seeding;
        public IdentityEngine(IIdentityRepository identityRepository,
            IMoneyAccountRepository MoneyAccountRepository,  ISeedingLists seeding)
        {
            this.identityRepository = identityRepository;
            this.MoneyAccountRepository = MoneyAccountRepository;
            this.seeding = seeding;
        }
        public string GetMoneyAccountId(string userName)
        {
            return identityRepository.GetUserId(userName);
        }

        public void InitializeNewUser(string userName)
        {
            var MoneyAccount = seeding.GetDefaultAccountForNewUser(GetMoneyAccountId(userName));
            MoneyAccountRepository.Insert(MoneyAccount);
            MoneyAccountRepository.Save();
        }
    }
}
