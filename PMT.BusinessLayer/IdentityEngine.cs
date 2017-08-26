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
        IMoneyAccountRepository moneyAccountRepository;
        ILogger logger;
        IPersonalizedSeeding personalizedSeeding;
        ITransactionsEngine transactionsEngine;
        public IdentityEngine(ILoggerFactory logger, IIdentityRepository identityRepository, ITransactionsEngine transactionsEngine,
            IMoneyAccountRepository moneyAccountRepository,  IPersonalizedSeeding personalizedSeeding)
        {
            this.identityRepository = identityRepository;
            this.moneyAccountRepository = moneyAccountRepository;
            this.personalizedSeeding = personalizedSeeding;
            this.transactionsEngine = transactionsEngine;
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

        public void InitializeNewUser(string userName, bool demoData)
        {
            try
            {
                var userId = GetUserId(userName);
                var moneyaccount=personalizedSeeding.GetDefaultAccountForNewUser(userId);
                moneyAccountRepository.Insert(moneyaccount);
                personalizedSeeding.Categories(userId);
                if (demoData) { 
                    var demoDataList = personalizedSeeding.GetDemoData(userId,moneyaccount.MoneyAccountId);
                    foreach (var transaction in demoDataList)
                        transactionsEngine.InsertNewTransaction(transaction);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, userName+": Add default account for new user");
            }
        }
    }
}
