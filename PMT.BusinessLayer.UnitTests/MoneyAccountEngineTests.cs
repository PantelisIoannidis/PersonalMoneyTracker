using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.UnitTests
{
    [TestFixture]
    public class MoneyAccountEngineTests
    {
        private Mock<ILoggerFactory> mockLogger;
        private ActionStatus actionStatus;
        private Mock<ITransactionRepository> mockTransactionRepository;
        private Mock<IMoneyAccountRepository> mockMoneyAccountRepository;
        private Mock<ICurrentDateTime> mockCurrentDateTime;
        private List<MoneyAccount> moneyAccountListUserX;
        private MoneyAccount amoneyAccount;
        private List<MoneyAccountVM> moneyAccountVMListUserX;

        [SetUp]
        public void Setup()
        {
            mockLogger = new Mock<ILoggerFactory>();
            actionStatus = new ActionStatus();
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockMoneyAccountRepository = new Mock<IMoneyAccountRepository>();
            mockCurrentDateTime = new Mock<ICurrentDateTime>();

            amoneyAccount = new MoneyAccount() {
                MoneyAccountId = 1,
                UserId = "user1",
                Balance = 1000.11m,
                Name = "name1",
            };

            moneyAccountListUserX = new List<MoneyAccount>() {
                new MoneyAccount() { MoneyAccountId = 1, UserId = "user1", Balance = 1000.11m, Name = "name1" },
                new MoneyAccount() { MoneyAccountId = 2, UserId = "user1", Balance = 2000.22m, Name = "name2" },
                new MoneyAccount() { MoneyAccountId = 3, UserId = "user1", Balance = 3000.33m, Name = "name3" },
            };

            moneyAccountVMListUserX = new List<MoneyAccountVM>() {
                new MoneyAccountVM() { MoneyAccountId = 1, UserId = "user1", Balance = 1000.11m, Name = "name1" },
                new MoneyAccountVM() { MoneyAccountId = 2, UserId = "user1", Balance = 2000.22m, Name = "name2" },
                new MoneyAccountVM() { MoneyAccountId = 3, UserId = "user1", Balance = 3000.33m, Name = "name3" },
            };

        }

        [Test]
        public void GetById_should_call_repository()
        {
            var userid = 0;

            mockMoneyAccountRepository.Setup(x => x.GetById(It.IsAny<int>()));

            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            moneyAccountEngine.GetById(userid);

            mockMoneyAccountRepository.VerifyAll();
        }

        [Test]
        public void AddNewAccountWithInitialBalance_should_call_repository()
        {
            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            moneyAccountEngine.AddNewAccountWithInitialBalance(amoneyAccount,0);

            mockMoneyAccountRepository.Verify(x => x.AddNewAccountWithInitialBalance(It.IsAny<MoneyAccount>(), It.IsAny<Transaction>()));
        }

        [Test]
        public void GetMoneyAccounts_should_call_repository_and_return_account_list()
        {
            string userId = "user1";

            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            mockMoneyAccountRepository.Setup(x => x.GetMoneyAccounts(It.Is<string>(y=>y==userId)))
                .Returns(moneyAccountListUserX);

            var moneyList = moneyAccountEngine.GetMoneyAccounts(userId);

            Assert.That(moneyList.Count(), Is.EqualTo(3));
            mockMoneyAccountRepository.Verify();
        }

        [Test]
        public void GetMoneyAccountsWithBalance_should_call_repository_and_return_account_list()
        {
            string userId = "user1";

            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            mockMoneyAccountRepository.Setup(x => x.GetMoneyAccountsWithBalance(It.Is<string>(y => y == userId)))
                .Returns(moneyAccountVMListUserX);

            var moneyAccountList = moneyAccountEngine.GetMoneyAccountsWithBalance(userId);

            Assert.That(moneyAccountList.Count(), Is.EqualTo(3));
            mockMoneyAccountRepository.Verify();
        }

        [Test]
        public void EditAccountNameAdjustBalance_should_call_repository()
        {
            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            moneyAccountEngine.EditAccountNameAdjustBalance(amoneyAccount, 0);

            mockMoneyAccountRepository.Verify(x => x.EditAccountNameAdjustBalance(It.IsAny<MoneyAccount>(), It.IsAny<Transaction>()));
        }

        [Test]
        public void GetMoneyAccountsPlusAll_should_return_all_user_accounts_plus_all_accounts_entry()
        {
            string userId = "user1";

            mockMoneyAccountRepository.Setup(x => x.GetMoneyAccounts(It.IsAny<string>()))
                .Returns(moneyAccountListUserX);

            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);


            var moneyAccountList = moneyAccountEngine.GetMoneyAccountsPlusAll(userId);

            Assert.That(moneyAccountList.FirstOrDefault().Name, Is.EqualTo(ViewText.AllAccounts));
            Assert.That(moneyAccountList.Count(), Is.EqualTo(4));
            mockMoneyAccountRepository.Verify();

        }

        [Test]
        public void GetMoneyAccountsExcludingCurrent_should_call_repository_return_listof_accounts()
        {
            var userId = "user1";
            var moneyAccountId = 0;

            mockMoneyAccountRepository.Setup(x => x.GetMoneyAccountsExcludingCurrent(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(moneyAccountListUserX);

            var moneyAccountEngine = new MoneyAccountEngine(mockLogger.Object, mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCurrentDateTime.Object, actionStatus);

            var moneyAccountList = moneyAccountEngine.GetMoneyAccountsExcludingCurrent(userId, moneyAccountId);

            Assert.IsTrue(moneyAccountList is List<MoneyAccount>);
            Assert.That(moneyAccountList.Count(), Is.GreaterThan(0));
            mockMoneyAccountRepository.VerifyAll();
        }


    }
}
