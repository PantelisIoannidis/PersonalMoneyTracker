using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.Common;
using PMT.Common.Helpers;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PMT.Entities.Literals;

namespace PMT.BusinessLayer
{
    public class ChartsEngine : BaseEngine, IChartsEngine
    {
        ILogger logger;
        ITransactionsEngine transactionsEngine;
        ITransactionRepository transactionRepository;
        IPeriod period;
        public ChartsEngine(ILoggerFactory logger,
                            IPeriod period,
                            ITransactionsEngine transactionsEngine,
                            ITransactionRepository transactionRepository)
        {
            this.logger = logger.CreateLogger<ChartsEngine>();
            this.transactionsEngine = transactionsEngine;
            this.transactionRepository = transactionRepository;
            this.period = period;
        }

        public string ChartIncomeVsExpense(string userId, TransactionFilterVM transactionFilterVM)
        {
            string data="";
            
            try
            {
                logger.LogError("testtest");
                ChartDataVM chartData = new ChartDataVM();
                ChartDatasetsVM chartDataset = new ChartDatasetsVM();
                period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);
                TransactionsSummaryVM summary = transactionsEngine.PrepareSummary(userId, transactionFilterVM);

                chartDataset.backgroundColor.Add("#008000");
                chartDataset.backgroundColor.Add("#b30000");
                chartDataset.borderColor.Add("#008010");
                chartDataset.borderColor.Add("#b30010");
                chartDataset.borderWidth = "1";
                chartDataset.labels = ViewText.IncomevsExpenses;
                chartDataset.data.Add(summary.Income.ToString("0.##"));
                chartDataset.data.Add(Math.Abs(summary.Expenses).ToString("0.##"));

                chartData.labels.Add(ViewText.Income);
                chartData.labels.Add(ViewText.Expense);
                chartData.datasets.Add(chartDataset);

                data = JsonConvert.SerializeObject(chartData);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.POPULATE_OBJECT, ex, "Prepare data for income vs expense chart");
            }
            return data;
        }
        public string ChartIncomeExpensesByCategory(string userId, TransactionFilterVM transactionFilterVM, TransactionType transactionType)
        {
            string data = "";
            try
            {
                const int capacity = 5;

                ChartDataVM chartData = new ChartDataVM();
                ChartDatasetsVM chartDataset = new ChartDatasetsVM();
                period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);

                var categories = transactionRepository.GetTransactionsGroupByCategory(userId, (Period)period, transactionFilterVM.AccountFilterId, transactionType).ToList();
                List<CategoryGroupByVM> topCategories = categories.Take(capacity).ToList();
                decimal otherSum = 0;
                if (categories.Count() > capacity)
                    otherSum = categories.Skip(capacity).Sum(x => x.Sum);
                CategoryGroupByVM otherCategories = new CategoryGroupByVM()
                {
                    Sum = otherSum,
                    IconId = DefaultCategoryValues.IconId,
                    Color = DefaultCategoryValues.Color,
                    Name = ViewText.Others
                };
                List<CategoryGroupByVM> finalListCategories = new List<CategoryGroupByVM>();
                finalListCategories.AddRange(topCategories);
                if (otherSum != 0)
                    finalListCategories.Add(otherCategories);

                foreach (var category in finalListCategories)
                {
                    chartDataset.backgroundColor.Add(category.Color);
                    chartDataset.borderColor.Add(category.Color);
                    chartDataset.data.Add(Math.Abs(category.Sum).ToString("0.##"));
                    chartData.labels.Add(category.Name);
                }

                chartDataset.labels = "";
                chartDataset.borderWidth = "1";

                chartData.datasets.Add(chartDataset);

                data = JsonConvert.SerializeObject(chartData);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.POPULATE_OBJECT, ex, "Prepare data for income and expense charts");
            }
            return data;

        }

    }
}
