using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common.Helpers;
using PMT.DataLayer.Repositories;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.UnitTests
{
    [TestFixture]
    public class ChartEngineTests
    {
        private Mock<ITransactionsEngine> mockTransactionsEngine;
        private Mock<ITransactionRepository> mockTransactionRepository;
        private Mock<ILoggerFactory> mockLogger;
        private TransactionsSummaryVM transactionsSummaryVM;
        private List<CategoryGroupByVM> transactionsGroupByCategory;

        [SetUp]
        public void Setup()
        {
            mockTransactionsEngine = new Mock<ITransactionsEngine>();
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockLogger = new Mock<ILoggerFactory>();

            transactionsSummaryVM = new TransactionsSummaryVM()
            {
                Income = 3000.33m,
                Expenses = 2000.22m,
                Balance = 1000.11m
            };

            transactionsGroupByCategory = new List<CategoryGroupByVM>()
            {
                new CategoryGroupByVM(){IconId="1",Name="Category_1",Color="#fff",Sum=10.11m },
                new CategoryGroupByVM(){IconId="2",Name="Category_2",Color="#fff",Sum=20.11m },
                new CategoryGroupByVM(){IconId="3",Name="Category_3",Color="#fff",Sum=30.11m },
                new CategoryGroupByVM(){IconId="4",Name="Category_4",Color="#fff",Sum=40.11m },
                new CategoryGroupByVM(){IconId="5",Name="Category_5",Color="#fff",Sum=50.11m },
                new CategoryGroupByVM(){IconId="6",Name="Category_6",Color="#fff",Sum=60.11m },
                new CategoryGroupByVM(){IconId="7",Name="Category_7",Color="#fff",Sum=70.11m },
            };
        }

        [Test]
        public void ChartIncomeVsExpense_should_return_json_with_summaries_and_chart_formatting()
        {
            string userId = "user1";

            mockTransactionsEngine.Setup(x => x.PrepareSummary(It.IsAny<string>(), It.IsAny<TransactionFilterVM>()))
                .Returns(()=> transactionsSummaryVM);


            var chartsEngine = new ChartsEngine(mockLogger.Object, new Period(), mockTransactionsEngine.Object, mockTransactionRepository.Object);

            var jsonData = chartsEngine.ChartIncomeVsExpense(userId, new TransactionFilterVM());

            var data = JsonConvert.DeserializeObject<ChartDataVM>(jsonData);

            Assert.AreEqual(double.Parse(data.datasets[0].data[0]), double.Parse("3000.33"));
            Assert.AreEqual(double.Parse(data.datasets[0].data[1]), double.Parse("2000.22"));
            Assert.That(string.IsNullOrEmpty(data.datasets[0].labels),Is.False, "label string must not be null or empty");
            Assert.That(data.labels.Count(),Is.EqualTo(2));

        }
        
        [Test]
        public void ChartIncomeExpensesByCategory_should_return_json_with_summaries_and_chart_formatting()
        {
            string userId = "user1";

            TransactionFilterVM transactionFilterVM = new TransactionFilterVM() {
               AccountFilterId = 1,
               PeriodFilterId = 0,
               SelectedDateFull="2010-10-01"
            };

            mockTransactionRepository.Setup(x => x.GetTransactionsGroupByCategory(
                It.IsAny<string>(),
                It.IsAny<Period>(),
                It.IsAny<int>(),
                It.IsAny<Entities.TransactionType>()))
                .Returns(() => transactionsGroupByCategory.AsQueryable());

            var chartsEngine = new ChartsEngine(mockLogger.Object, new Period(), mockTransactionsEngine.Object, mockTransactionRepository.Object);

            var jsonData = chartsEngine.ChartIncomeExpensesByCategory(userId, transactionFilterVM, Entities.TransactionType.Income);

            var data = JsonConvert.DeserializeObject<ChartDataVM>(jsonData);

            Assert.AreEqual(double.Parse(data.datasets[0].data[0]), double.Parse("10.11"));
            Assert.AreEqual(double.Parse(data.datasets[0].data[5]), double.Parse("130.22"));
            Assert.That(data.labels.Count(), Is.EqualTo(6));
        }

    }
}
