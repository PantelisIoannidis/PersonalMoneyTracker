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
    public class TransactionsEngine : ITransactionsEngine
    {
        ILogger logger;
        IActionStatus actionStatus;
        IMoneyAccountEngine moneyAccountEngine;
        ITransactionRepository transactionRepository;
        public TransactionsEngine(ILoggerFactory logger,
            IActionStatus actionStatus,
            IMoneyAccountEngine moneyAccountEngine,
            ITransactionRepository transactionRepository)
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<TransactionsEngine>();
            this.moneyAccountEngine = moneyAccountEngine;
            this.transactionRepository = transactionRepository;
        }

        public TransactionFilterVM GetFilter(string userId, string objPreferences)
        {
            TransactionFilterVM transactionFilterVM = new TransactionFilterVM();
            DateTime selectedDate = DateTime.Now;
            Period period;

            if (!string.IsNullOrEmpty(objPreferences))
            {
                
                var pref = JsonConvert.DeserializeObject<TransactionsFilterPreferences>(objPreferences);
                period = new Period(DateTime.Parse(pref.SelectedDateFull), (PeriodType)pref.PeriodFilterId);
                if (pref.Operation==TransactionFilterOperation.MoveToNext)
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
                period = new Period(DateTime.Parse(transactionFilterVM.SelectedDateFull), PeriodType.Week);
                transactionFilterVM.PeriodDescription = period.GetDescription();
            }

            transactionFilterVM.PeriodEnum = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToDictionary(e => (int)e, e => e.ToString());
            transactionFilterVM.MoneyAccountChoiceFilter = moneyAccountEngine.GetMoneyAccountsPlusAll(userId);

            return transactionFilterVM;
        }

        public TransactionsSummaryVM PrepareSummary(string userId, TransactionFilterVM transactionFilterVM)
        {
            Period period = new Period(DateTime.Parse(transactionFilterVM.SelectedDateFull), (PeriodType)transactionFilterVM.PeriodFilterId);
            var income = transactionRepository.GetBalance(userId, transactionFilterVM.AccountFilterId, period, TransactionType.Income);
            var expenses = transactionRepository.GetBalance(userId, transactionFilterVM.AccountFilterId, period, TransactionType.Expense);
            var Balance = income + expenses;
            var summary = new TransactionsSummaryVM()
            {
                Income = income,
                Expenses = expenses,
                Balance = Balance
            };

            return summary;
        }
    }
}
