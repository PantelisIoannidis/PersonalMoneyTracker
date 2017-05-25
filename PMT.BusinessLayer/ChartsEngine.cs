using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.Common;
using PMT.Common.Helpers;
using PMT.Common.Resources;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    
    public class ChartsEngine : IChartsEngine
    {
        ILogger logger;
        ITransactionsEngine transactionsEngine;
        public ChartsEngine(ILoggerFactory logger,
                            ITransactionsEngine transactionsEngine)
        {
            this.logger = logger.CreateLogger<ChartsEngine>();
            this.transactionsEngine = transactionsEngine;
        }

        public string ChartIncomeVsExpense(string userId,TransactionFilterVM transactionFilterVM) {
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

            chartDataset.data.Add(summary.Income.ToString());
            chartDataset.data.Add(Math.Abs(summary.Expenses).ToString());

            chartData.labels.Add(ViewText.Income);
            chartData.labels.Add(ViewText.Expenses);

            chartData.datasets.Add(chartDataset);

            return JsonConvert.SerializeObject(chartData);

        }

    }
}
