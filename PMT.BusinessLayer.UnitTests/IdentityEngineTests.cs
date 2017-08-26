using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seed;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.UnitTests
{
    [TestFixture]
    public class IdentityEngineTests
    {
        private Mock<IIdentityRepository> mockIdentityRepository;
        private Mock<IMoneyAccountRepository> mockMoneyAccountRepository;
        private Mock<IPersonalizedSeeding> mockPersonalizedSeeding;
        private Mock<ITransactionsEngine> mockTransactionsEngine;
        private Mock<ILoggerFactory> mockLogger;
        private List<Transaction> transactionsList;

        [SetUp]
        public void Setup()
        {
            mockIdentityRepository = new Mock<IIdentityRepository>();
            mockMoneyAccountRepository = new Mock<IMoneyAccountRepository>();
            mockPersonalizedSeeding = new Mock<IPersonalizedSeeding>();
            mockTransactionsEngine = new Mock<ITransactionsEngine>();
            mockLogger = new Mock<ILoggerFactory>();
            transactionsList = new List<Transaction>() {
                new Transaction(),
                new Transaction(),
                new Transaction(),
            };
        }

        [Test]
        public void GetUserId_with_input_name_should_return_userId()
        {
            string userName = "userName1";
            string userId = "user1";

            mockIdentityRepository.Setup(x => x.GetUserId(It.IsAny<string>()))
                .Returns(userId);

            var identityEngine = new IdentityEngine(mockLogger.Object,mockIdentityRepository.Object, mockTransactionsEngine.Object, mockMoneyAccountRepository.Object, mockPersonalizedSeeding.Object);

            var resultUserId=identityEngine.GetUserId(userName);

            mockIdentityRepository.VerifyAll();
            Assert.AreEqual(userId, resultUserId);
        }

        [Test]
        public void InitializeNewUser_should_insert_initial_entries_for_new_user_in_database()
        {
            string userName = "userName1";
            string userId = "user1";
            bool demoData = true;

            mockIdentityRepository.Setup(x => x.GetUserId(It.IsAny<string>()))
                .Returns(userId);

            mockPersonalizedSeeding.Setup(x => x.GetDefaultAccountForNewUser(It.IsAny<string>()))
                .Returns(()=> new MoneyAccount { UserId = userId, MoneyAccountId = 0, Name = ViewText.Personal });

            mockPersonalizedSeeding.Setup(x => x.GetDemoData(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(transactionsList);

            mockMoneyAccountRepository.Setup(x => x.Insert(It.IsAny<MoneyAccount>()));

            mockTransactionsEngine.Setup(x => x.InsertNewTransaction(It.IsAny<Transaction>()));

            var identityEngine = new IdentityEngine(mockLogger.Object, mockIdentityRepository.Object, mockTransactionsEngine.Object, mockMoneyAccountRepository.Object, mockPersonalizedSeeding.Object);

            identityEngine.InitializeNewUser(userName, demoData);

            mockIdentityRepository.VerifyAll();
            mockPersonalizedSeeding.VerifyAll();
            mockMoneyAccountRepository.VerifyAll();
            mockTransactionsEngine.Verify(x => x.InsertNewTransaction(It.IsAny<Transaction>())
                                            ,Times.Exactly(3));


        }
    }
}
