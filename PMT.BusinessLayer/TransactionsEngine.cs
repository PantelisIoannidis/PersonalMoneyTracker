﻿using Microsoft.Extensions.Logging;
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
using System.Resources;
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
        ICurrentDateTime currentDateTime;
        public TransactionsEngine(ILoggerFactory logger,
                                    IPeriod period,
                                    IActionStatus actionStatus,
                                    IMoneyAccountEngine moneyAccountEngine,
                                    IMoneyAccountRepository moneyAccountRepository,
                                    ITransactionRepository transactionRepository,
                                    ICategoriesEngine categoriesEngine,
                                    ICurrentDateTime currentDateTime)
        {
            this.actionStatus = actionStatus;
            this.period = period;
            this.logger = logger.CreateLogger<TransactionsEngine>();
            this.moneyAccountEngine = moneyAccountEngine;
            this.moneyAccountRepository = moneyAccountRepository;
            this.transactionRepository = transactionRepository;
            this.categoriesEngine = categoriesEngine;
            this.currentDateTime = currentDateTime;
        }

        public decimal GetBalance(string userId)
        {
            return transactionRepository.GetBalance(userId);
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

        public TransactionFilterVM GetFilter(string userId, string objPreferences, int timeZoneOffset = 0)
        {
            TransactionFilterVM transactionFilterVM = new TransactionFilterVM();
            DateTime selectedDate = currentDateTime.DateTimeUtcNow().ToLocalTime(timeZoneOffset);

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
                        period.ResetSelectedDate(currentDateTime.DateTimeUtcNow().ToLocalTime(timeZoneOffset));

                    transactionFilterVM.PeriodFilterId = pref.PeriodFilterId;
                    transactionFilterVM.AccountFilterId = pref.AccountFilterId;
                    transactionFilterVM.SelectedDateFull = period.SelectedDate.ToString("s");
                    transactionFilterVM.PeriodDescription = period.GetDescription();
                    selectedDate = period.SelectedDate;
                }
                else
                {
                    transactionFilterVM.AccountFilterId = AccountType.All;
                    transactionFilterVM.PeriodFilterId = (int)PeriodType.Month;
                    transactionFilterVM.SelectedDateFull = selectedDate.ToString("s");
                    period.Init(DateTime.Parse(transactionFilterVM.SelectedDateFull), PeriodType.Month);
                    transactionFilterVM.PeriodDescription = period.GetDescription();
                }
                
                var periods = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToDictionary(e => (int)e, e => e.ToString());
                foreach(var period in periods)
                {
                    var translationFromResources = ModelText.ResourceManager.GetString("Period" + period.Value);
                    transactionFilterVM.PeriodEnum.Add(period.Key, translationFromResources);
                }

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
                var adjustments = transactionRepository.GetBalance(userId, transactionFilterVM.AccountFilterId, (Period)period, TransactionType.Adjustment);
                var Balance = income - expenses + adjustments;

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

        public TransactionVM GetTransactionVM(int transactionId)
        {
            return transactionRepository.GetTransactionVM(transactionId);
        }

        public IQueryable<TransactionVM> GetTransactionsVM(string userId, Period period, int account)
        {
            return transactionRepository.GetTransactionsVM(userId, period, account);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            transactionRepository.Update(transaction);
        }

        public Transaction GetTransactionById(int id)
        {
            return transactionRepository.GetById(id);
        }

        public void DeleteTransaction(int id)
        {
            transactionRepository.Delete(id);
        }
    }
}
