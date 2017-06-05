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
        IMoneyAccountRepository moneyAccountRepository;
        ICategoriesEngine categoriesEngine;
        public TransactionsEngine(ILoggerFactory logger,
                                    IPeriod period,
                                    IActionStatus actionStatus,
                                    IMoneyAccountEngine moneyAccountEngine,
                                    IMoneyAccountRepository moneyAccountRepository,
                                    ITransactionRepository transactionRepository,
                                    ICategoriesEngine categoriesEngine)
        {
            this.actionStatus = actionStatus;
            this.period = period;
            this.logger = logger.CreateLogger<TransactionsEngine>();
            this.moneyAccountEngine = moneyAccountEngine;
            this.moneyAccountRepository = moneyAccountRepository;
            this.transactionRepository = transactionRepository;
            this.categoriesEngine = categoriesEngine;
        }

        public ActionStatus InsertNewTransaction(Transaction transaction)
        {

            var specialCategories = categoriesEngine.GetAllSpecialGategoriesSubCategories(transaction.UserId);
            try
            {
                if (transaction.TransactionType == TransactionType.Adjustment)
                {
                    if (transaction.Amount >= 0) { 
                        transaction.TransactionType = TransactionType.Income;
                        transaction.CategoryId = specialCategories.FirstOrDefault(x=>x.SpecialAttribute==SpecialAttributes.AdjustmentIncome).CategoryId;
                        transaction.SubCategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.AdjustmentIncome)
                            .SubCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.AdjustmentIncome).SubCategoryId;
                    }
                    else { 
                        transaction.TransactionType = TransactionType.Expense;
                        transaction.CategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.AdjustmentExpense).CategoryId;
                        transaction.SubCategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.AdjustmentExpense)
                            .SubCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.AdjustmentExpense).SubCategoryId;
                    }


                }

                if (transaction.TransactionType==TransactionType.Income
                    || transaction.TransactionType == TransactionType.Expense)
                {
                    transactionRepository.Insert(transaction);
                    transactionRepository.Save();
                }

                if (transaction.TransactionType == TransactionType.Transfer)
                {
                    var transactionFrom = new Transaction() {
                        TransactionType=TransactionType.Expense,
                        Amount=transaction.Amount,
                        Description=""
                            + moneyAccountRepository.GetMoneyAccount(transaction.UserId,transaction.MoneyAccountId).Name,
                        MoneyAccountId=transaction.TransferTo.Value,
                        TransactionDate=transaction.TransactionDate,
                        UserId=transaction.UserId,
                        CategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferExpense).CategoryId,
                        SubCategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferExpense)
                            .SubCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferExpense).SubCategoryId
                    };
                    var transactionTo = new Transaction()
                    {
                        TransactionType = TransactionType.Income,
                        Amount = transaction.Amount,
                        Description = "" 
                            + moneyAccountRepository.GetMoneyAccount(transaction.UserId, transaction.TransferTo.Value).Name,
                        MoneyAccountId = transaction.MoneyAccountId,
                        TransactionDate = transaction.TransactionDate,
                        UserId = transaction.UserId,
                        CategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferIncome).CategoryId,
                        SubCategoryId = specialCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferIncome)
                            .SubCategories.FirstOrDefault(x => x.SpecialAttribute == SpecialAttributes.TransferIncome).SubCategoryId
                    };

                    transactionRepository.Insert(transactionFrom);
                    transactionRepository.Insert(transactionTo);
                    transactionRepository.Save();
                }
                
            }
            catch(Exception ex)
            {
                var message = "Call repository. Insert new transaction";
                actionStatus = ActionStatus.CreateFromException(message, ex);
                logger.LogError(LoggingEvents.CALL_METHOD, ex, message);
            }

            return (ActionStatus)actionStatus;
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
