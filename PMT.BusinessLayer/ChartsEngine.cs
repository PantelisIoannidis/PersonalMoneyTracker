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

    public class ChartsEngine : IChartsEngine
    {
        ILogger logger;
        ITransactionsEngine transactionsEngine;
        ITransactionRepository transactionRepository;
        public ChartsEngine(ILoggerFactory logger,
                            ITransactionsEngine transactionsEngine,
                            ITransactionRepository transactionRepository)
        {
            this.logger = logger.CreateLogger<ChartsEngine>();
            this.transactionsEngine = transactionsEngine;
            this.transactionRepository = transactionRepository;
        }

        public string ChartIncomeVsExpense(string userId, TransactionFilterVM transactionFilterVM)
        {
            ChartDataVM chartData = new ChartDataVM();
            ChartDatasetsVM chartDataset = new ChartDatasetsVM();
            Period period = new Period(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);
            TransactionsSummaryVM summary = transactionsEngine.PrepareSummary(userId, transactionFilterVM);

            chartDataset.backgroundColor.Add("#008000");
            chartDataset.backgroundColor.Add("#b30000");

            chartDataset.borderColor.Add("#008010");
            chartDataset.borderColor.Add("#b30010");

            chartDataset.borderWidth = "1";

            chartDataset.labels = "Income vs Expenses";

            chartDataset.data.Add(summary.Income.ToString("0.##"));
            chartDataset.data.Add(Math.Abs(summary.Expenses).ToString("0.##"));

            chartData.labels.Add(ViewText.Income);
            chartData.labels.Add(ViewText.Expense);

            chartData.datasets.Add(chartDataset);

            return JsonConvert.SerializeObject(chartData);

        }
        public string ChartIncomeExpensesByCategory(string userId, TransactionFilterVM transactionFilterVM,TransactionType transactionType)
        {
            const int capacity = 5;

            ChartDataVM chartData = new ChartDataVM();
            ChartDatasetsVM chartDataset = new ChartDatasetsVM();
            Period period = new Period(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);

            var transactions = transactionRepository.GetTransactionsGroupByCategory(userId, period, transactionFilterVM.AccountFilterId, transactionType).ToList();

            List<TransactionGroupByVM> namedTrans = transactions.Take(capacity).ToList();
            decimal otherSum=0;
            if (transactions.Count()> capacity)
                otherSum = transactions.Skip(capacity).Sum(x => x.Sum);
            TransactionGroupByVM otherTransactions = new TransactionGroupByVM()
            {
                Sum = otherSum,
                IconId = DefaultOtherTransactions.IconId,
                Color = DefaultOtherTransactions.Color,
                Name = ViewText.Others
            };
            List<TransactionGroupByVM> trans = new List<TransactionGroupByVM>();
            trans.AddRange(namedTrans);
            if(otherSum!=0)
                trans.Add(otherTransactions);

            foreach (var tran in trans)
            {
                chartDataset.backgroundColor.Add(tran.Color);
                chartDataset.borderColor.Add(tran.Color);
                chartDataset.data.Add(Math.Abs(tran.Sum).ToString("0.##"));
                chartData.labels.Add(tran.Name);
            }

            chartDataset.labels = "";
            chartDataset.borderWidth = "1";

            chartData.datasets.Add(chartDataset);

            return JsonConvert.SerializeObject(chartData);

        }

    }
}
