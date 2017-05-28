using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.Common;
using PMT.Common.Helpers;
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
    public class TransactionsEngine : BaseEngine, ITransactionsEngine
    {
        ILogger logger;
        IPeriod period;
        IActionStatus actionStatus;
        IMoneyAccountEngine moneyAccountEngine;
        ITransactionRepository transactionRepository;
        public TransactionsEngine(ILoggerFactory logger,
                                    IPeriod period,
                                    IActionStatus actionStatus,
                                    IMoneyAccountEngine moneyAccountEngine,
                                    ITransactionRepository transactionRepository)
        {
            this.actionStatus = actionStatus;
            this.period = period;
            this.logger = logger.CreateLogger<TransactionsEngine>();
            this.moneyAccountEngine = moneyAccountEngine;
            this.transactionRepository = transactionRepository;
        }

        public TransactionFilterVM GetFilter(string userId, string objPreferences)
        {
            TransactionFilterVM transactionFilterVM = new TransactionFilterVM();
            DateTime selectedDate = DateTime.Now;

            try
            {
                if (!string.IsNullOrEmpty(objPreferences))
                {
                    var pref = JsonConvert.DeserializeObject<TransactionsFilterPreferences>(objPreferences);
                    period.Init(DateTime.Parse(pref.SelectedDateFull), (PeriodType)pref.PeriodFilterId);

                    if (pref.Operation == TransactionFilterOperation.MoveToNext)
                        period.MoveToNext();
                    if (pref.Operation == TransactionFilterOperation.MoveToPrevious)
                        period.MoveToPrevious();
                    if (pref.Operation == TransactionFilterOperation.Reset)
                        period.ResetSelectedDate(DateTime.Now);

                    transactionFilterVM.PeriodFilterId = pref.PeriodFilterId;
                    transactionFilterVM.AccountFilterId = pref.AccountFilterId;
                    transactionFilterVM.SelectedDateFull = period.SelectedDate.ToString("s");
                    transactionFilterVM.PeriodDescription = period.GetDescription();
                    selectedDate = period.SelectedDate;
                }
                else
                {
                    transactionFilterVM.AccountFilterId = AccountType.All;
                    transactionFilterVM.PeriodFilterId = (int)PeriodType.Week;
                    transactionFilterVM.SelectedDateFull = selectedDate.ToString("s");
                    period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), PeriodType.Week);
                    transactionFilterVM.PeriodDescription = period.GetDescription();
                }
                transactionFilterVM.PeriodEnum = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToDictionary(e => (int)e, e => e.ToString());
                transactionFilterVM.MoneyAccountChoiceFilter = moneyAccountEngine.GetMoneyAccountsPlusAll(userId);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.POPULATE_OBJECT, ex, "Populate transactions filter view model");
            }
            return transactionFilterVM;
        }

        public TransactionsSummaryVM PrepareSummary(string userId, TransactionFilterVM transactionFilterVM)
        {
            try
            {
                period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);
                var income = transactionRepository.GetBalance(userId, transactionFilterVM.AccountFilterId, (Period)period, TransactionType.Income);
                var expenses = transactionRepository.GetBalance(userId, transactionFilterVM.AccountFilterId, (Period)period, TransactionType.Expense);
                var Balance = income + expenses;
                var summary = new TransactionsSummaryVM()
                {
                    Income = income,
                    Expenses = expenses,
                    Balance = Balance
                };
                return summary;
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.POPULATE_OBJECT, ex, "Populate transactions summer view model");
                return new TransactionsSummaryVM();
            }
        }
    }
}
