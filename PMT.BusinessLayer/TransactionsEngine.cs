using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMT.Common;
using PMT.Common.Helpers;
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
        public TransactionsEngine(ILoggerFactory logger,
            IActionStatus operationStatus,
            IMoneyAccountEngine moneyAccountEngine)
        {
            this.actionStatus = operationStatus;
            this.logger = logger.CreateLogger<TransactionsEngine>();
            this.moneyAccountEngine = moneyAccountEngine;
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
                if (pref.MoveToNextFlag > 0)
                    period.MoveToNext();
                if (pref.MoveToNextFlag < 0)
                    period.MoveToPrevious();
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
    }
}
