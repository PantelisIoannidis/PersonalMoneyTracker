using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class IdentityEngine : BaseEngine, IIdentityEngine
    {
        IIdentityRepository identityRepository;
        IMoneyAccountRepository MoneyAccountRepository;
        ISeedingLists seedingLists;
        ILogger logger;
        public IdentityEngine(ILoggerFactory logger, IIdentityRepository identityRepository,
            IMoneyAccountRepository MoneyAccountRepository, ISeedingLists seedingLists)
        {
            this.identityRepository = identityRepository;
            this.MoneyAccountRepository = MoneyAccountRepository;
            this.seedingLists = seedingLists;
            this.logger = logger.CreateLogger<IdentityEngine>();
        }

        public string GetUserId(string userName)
        {
            try
            {
                return identityRepository.GetUserId(userName);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, userName + ": Get UserId");
                return "";
            }
        }

        public void InitializeNewUser(string userName)
        {
            try
            {
                var moneyAccount = seedingLists.GetDefaultAccountForNewUser(GetUserId(userName));
                MoneyAccountRepository.Insert(moneyAccount);
                MoneyAccountRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, userName+": Add default account for new user");
            }
        }
    }
}
