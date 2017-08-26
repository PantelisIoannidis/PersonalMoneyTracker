using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.Common.Helpers;
using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seed;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PMT.Entities.Literals;

namespace PMT.UnitTests
{
    [TestFixture]
    public class TransactionsEngineTests
    {
        private Mock<ILoggerFactory> mockLogger;
        IActionStatus actionStatus;
        private Mock<IMoneyAccountEngine> mockMoneyAccountEngine;
        private Mock<ITransactionRepository> mockTransactionRepository;
        private Mock<IMoneyAccountRepository> mockMoneyAccountRepository;
        private Mock<ICategoriesEngine> mockCategoriesEngine;
        private Mock<ICurrentDateTime> mockCurrentDateTime;
        private object testPeriod;
        private List<Category> categoriesList;
        private List<SubCategory> subCategoriesList;
        private List<Category> specialCategories;
        private List<SubCategory> specialSubCategories;
        private Mock<IPeriod> mockPeriod;
        private string testDateString;
        private int timeZoneOffset;

        public TransactionsEngineTests()
        {
            SeedingLists seedingLists = new SeedingLists();
            categoriesList = seedingLists.GetMainCategories();
            subCategoriesList = seedingLists.GetSubCategries();
            specialCategories = categoriesList
                .Where(x => !string.IsNullOrEmpty(x.SpecialAttribute)).ToList();
            specialSubCategories = subCategoriesList
                .Where(x => !string.IsNullOrEmpty(x.SpecialAttribute)).ToList();
            foreach (var category in categoriesList)
            {
                category.SubCategories = specialSubCategories;
            }
        }

        [SetUp]
        public void Setup()
        {
            mockLogger = new Mock<ILoggerFactory>();
            actionStatus = new ActionStatus();
            mockMoneyAccountEngine = new Mock<IMoneyAccountEngine>();
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockMoneyAccountRepository = new Mock<IMoneyAccountRepository>();
            mockCategoriesEngine = new Mock<ICategoriesEngine>();
            mockCurrentDateTime = new Mock<ICurrentDateTime>();
            testDateString = "2010-12-15";
            mockCurrentDateTime.Setup(x => x.DateTimeUtcNow())
                .Returns(new DateTime(2010, 12, 15));
            testPeriod = new Period()
            {
                Type = PeriodType.Month,
                SelectedDate = new DateTime(2010, 12, 15),
                FromDate = new DateTime(2010, 12, 1),
                ToDate = new DateTime(2010, 12, 31)
            };
            mockPeriod = new Mock<IPeriod>();
            timeZoneOffset = 0;
        }

        [Test]
        public void GetBalance_should_call_repository_method()
        {
            var userId = "user1";

            mockTransactionRepository.Setup(x => x.GetBalance(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Period>()))
                .Returns(1.1m);

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.GetBalance(userId);

            mockTransactionRepository.VerifyAll();
        }

        [Test]
        public void InsertNewTransaction_type_income_should_insert_transaction_in_database()
        {

            var transaction = new Transaction() {
                TransactionType=TransactionType.Income
            };

            mockCategoriesEngine.Setup(x => x.GetAllSpecialGategoriesSubCategories(It.IsAny<string>()))
                .Returns(specialCategories);

            mockTransactionRepository.Setup(x => x.Insert(It.IsAny<Transaction>()));

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object, 
                                    mockCurrentDateTime.Object);

            transactionEngine.InsertNewTransaction(transaction);

            mockTransactionRepository.VerifyAll();

        }

        [Test]
        public void InsertNewTransaction_type_expense_should_insert_transaction_in_database()
        {

            var transaction = new Transaction()
            {
                TransactionType = TransactionType.Expense
            };

            mockCategoriesEngine.Setup(x => x.GetAllSpecialGategoriesSubCategories(It.IsAny<string>()))
                .Returns(specialCategories);

            mockTransactionRepository.Setup(x => x.Insert(It.IsAny<Transaction>()));

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            transactionEngine.InsertNewTransaction(transaction);

            mockTransactionRepository.VerifyAll();

        }

        [Test]
        public void InsertNewTransaction_type_Adjustment_positive_amount_should_insert_transaction_as_special_income_in_database()
        {

            var transaction = new Transaction()
            {
                TransactionType = TransactionType.Adjustment,
                Amount=100
            };

            mockCategoriesEngine.Setup(x => x.GetAllSpecialGategoriesSubCategories(It.IsAny<string>()))
                .Returns(specialCategories);

            mockTransactionRepository.Setup(x => x.Insert(It.IsAny<Transaction>()));

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            transactionEngine.InsertNewTransaction(transaction);

            Assert.IsTrue(transaction.TransactionType== TransactionType.Income);
            mockTransactionRepository.VerifyAll();

        }

        [Test]
        public void InsertNewTransaction_type_Adjustment_negative_amount_should_insert_transaction_as_special_expense_in_database()
        {

            var transaction = new Transaction()
            {
                TransactionType = TransactionType.Adjustment,
                Amount = -100
            };

            mockCategoriesEngine.Setup(x => x.GetAllSpecialGategoriesSubCategories(It.IsAny<string>()))
                .Returns(specialCategories);

            mockTransactionRepository.Setup(x => x.Insert(It.IsAny<Transaction>()));

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            transactionEngine.InsertNewTransaction(transaction);

            Assert.IsTrue(transaction.TransactionType == TransactionType.Expense);
            mockTransactionRepository.VerifyAll();

        }

        [Test]
        public void InsertNewTransaction_type_transfer_should_insert_one_transaction_From_and_one_To_in_database()
        {

            var transaction = new Transaction()
            {
                TransactionType = TransactionType.Transfer,
                TransferTo=999
            };

            var moneyAccount = new MoneyAccount() {
                MoneyAccountId=1,
                Name="name1"
            };

            mockCategoriesEngine.Setup(x => x.GetAllSpecialGategoriesSubCategories(It.IsAny<string>()))
                .Returns(specialCategories);

            mockTransactionRepository.Setup(x => x.Insert(It.IsAny<Transaction>()));
            mockMoneyAccountRepository.Setup(x => x.GetMoneyAccount(It.IsAny<string>(), It.IsAny<int>()))
                                        .Returns(moneyAccount);

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            transactionEngine.InsertNewTransaction(transaction);

            mockTransactionRepository.Verify(x => x.Insert(It.IsAny<Transaction>())
                                            ,Times.Exactly(2));
        }

        [Test]
        public void GetFilter_with_preference_operation_moveToNext_should_call_period_moveToNext_to_move_date_forward()
        {
            var userId = "user1";
            TransactionsFilterPreferences transactionsFilterPreferences = new TransactionsFilterPreferences()
            {
                UserId= userId,
                Operation= TransactionFilterOperation.MoveToNext,
                SelectedDateFull=testDateString,
                AccountFilterId=1,
                PeriodFilterId= (int)PeriodType.Month
            };

            var json = JsonConvert.SerializeObject(transactionsFilterPreferences);

            var transactionEngine = new TransactionsEngine(mockLogger.Object, mockPeriod.Object, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var getFiler = transactionEngine.GetFilter(userId, json, timeZoneOffset);

            mockPeriod.Verify(x => x.MoveToNext());
        }

        [Test]
        public void GetFilter_with_preference_operation_moveToPrevious_should_call_period_moveToPrevious_to_move_date_backward()
        {
            var userId = "user1";
            TransactionsFilterPreferences transactionsFilterPreferences = new TransactionsFilterPreferences()
            {
                UserId = userId,
                Operation = TransactionFilterOperation.MoveToPrevious,
                SelectedDateFull = testDateString,
                AccountFilterId = 1,
                PeriodFilterId = (int)PeriodType.Month
            };

            var json = JsonConvert.SerializeObject(transactionsFilterPreferences);

            var transactionEngine = new TransactionsEngine(mockLogger.Object, mockPeriod.Object, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var getFiler = transactionEngine.GetFilter(userId, json, timeZoneOffset);

            mockPeriod.Verify(x => x.MoveToPrevious());
        }

        [Test]
        public void GetFilter_with_preference_operation_reset_should_call_period_reset_to_set_today_as_currentdate()
        {
            var userId = "user1";
            TransactionsFilterPreferences transactionsFilterPreferences = new TransactionsFilterPreferences()
            {
                UserId = userId,
                Operation = TransactionFilterOperation.Reset,
                SelectedDateFull = testDateString,
                AccountFilterId = 1,
                PeriodFilterId = (int)PeriodType.Month
            };

            var json = JsonConvert.SerializeObject(transactionsFilterPreferences);

            var transactionEngine = new TransactionsEngine(mockLogger.Object, mockPeriod.Object, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var getFilter = transactionEngine.GetFilter(userId, json, timeZoneOffset);


            mockPeriod.Verify(x => x.ResetSelectedDate(It.IsAny<DateTime>()));
        }

        [Test]
        public void GetFilter_with_null_preferences_should_choose_default_values()
        {
            var userId = "user1";
            TransactionsFilterPreferences transactionsFilterPreferences=null;

            string json = null;

            var transactionEngine = new TransactionsEngine(mockLogger.Object, mockPeriod.Object, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var getFilter = transactionEngine.GetFilter(userId, json, timeZoneOffset);


            Assert.IsTrue(getFilter.AccountFilterId == AccountType.All);
            Assert.IsTrue(getFilter.PeriodFilterId == (int)PeriodType.Month);
        }

        [Test]
        public void GetFilter__should_prepare_PeriodEnum_list()
        {
            var userId = "user1";
            TransactionsFilterPreferences transactionsFilterPreferences = null;

            string json = null;

            var transactionEngine = new TransactionsEngine(mockLogger.Object, mockPeriod.Object, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var getFilter = transactionEngine.GetFilter(userId, json, timeZoneOffset);
            
            var periodElements = Enum.GetNames(typeof(PeriodType));
            var periodElementsCount = periodElements.Count();

            for(int i=0;i<periodElementsCount;i++)
            {
                Assert.IsTrue(periodElements[i] == getFilter.PeriodEnum[i]);
            }
            Assert.IsTrue(periodElementsCount == getFilter.PeriodEnum.Count());
        }

        [Test]
        public void PrepareSummary_should_prepare_TransactionsSummaryVM()
        {
            var userId = "user1";
            decimal income = 3000.33m;
            decimal expense = 1000.11m;
            decimal balance = 2000.22m;
            var transactionFilterVM = new TransactionFilterVM()
            {
                AccountFilterId=1,
                SelectedDateFull=testDateString,
                PeriodFilterId=(int)PeriodType.Month
            };


            mockTransactionRepository.Setup(x => x.GetBalance(It.IsAny<string>(),It.IsAny<int>(),It.IsAny<Period>(),
                It.Is<TransactionType>(y=>y==TransactionType.Income)))
                .Returns(income);

            mockTransactionRepository.Setup(x => x.GetBalance(It.IsAny<string>(),It.IsAny<int>(),It.IsAny<Period>(),
                It.Is<TransactionType>(y => y == TransactionType.Expense)))
                .Returns(expense);

            var transactionEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object,
                                    mockMoneyAccountRepository.Object, mockTransactionRepository.Object, mockCategoriesEngine.Object,
                                    mockCurrentDateTime.Object);

            var summary = transactionEngine.PrepareSummary(userId, transactionFilterVM);

            Assert.IsTrue(income == summary.Income);
            Assert.IsTrue(expense == summary.Expenses);
            Assert.IsTrue(balance == summary.Balance);
        }

        [Test]
        public void GetTransactionVM_should_call_repository_method()
        {
            var transactionId = 1;

            mockTransactionRepository.Setup(x => x.GetTransactionVM(It.IsAny<int>()));

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.GetTransactionVM(transactionId);

            mockTransactionRepository.VerifyAll();
        }

        [Test]
        public void GetTransactionsVM_should_call_repository_method()
        {
            var userId = "user1";
            var transactionId = 1;

            mockTransactionRepository.Setup(x => x.GetTransactionsVM(It.IsAny<string>(),It.IsAny<Period>(),It.IsAny<int>()));

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.GetTransactionsVM(userId,(Period)testPeriod, transactionId);

            mockTransactionRepository.VerifyAll();
        }

        [Test]
        public void UpdateTransaction_should_call_repository_method()
        {

            mockTransactionRepository.Setup(x => x.Update(It.IsAny<Transaction>()));

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.UpdateTransaction(new Transaction());

            mockTransactionRepository.VerifyAll();
        }

        [Test]
        public void GetTransactionById_should_call_repository_method()
        {
            var transactionId = 1;
            mockTransactionRepository.Setup(x => x.GetById(It.IsAny<int>()));

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.GetTransactionById(transactionId);

            mockTransactionRepository.VerifyAll();
        }

        [Test]
        public void DeleteTransaction_should_call_repository_method()
        {
            var transactionId = 1;
            mockTransactionRepository.Setup(x => x.Delete(It.IsAny<int>()));

            var transactionsEngine = new TransactionsEngine(mockLogger.Object, (IPeriod)testPeriod, actionStatus, mockMoneyAccountEngine.Object, mockMoneyAccountRepository.Object,
                                         mockTransactionRepository.Object, mockCategoriesEngine.Object, mockCurrentDateTime.Object);

            transactionsEngine.DeleteTransaction(transactionId);

            mockTransactionRepository.VerifyAll();
        }

    }
}
