﻿using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Common.Helpers;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PMT.Entities.Literals;

namespace PMT.DataLayer.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        MainDb db;
        public TransactionRepository(MainDb db,ILoggerFactory logger, IActionStatus actionStatus)
        {
            this.db = db;
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<TransactionRepository>();
        }

        public IQueryable<TransactionVM> GetTransactionsVM(string userId,Period period)
        {
            var transactions = from tran in db.Transactions
                               join cat in db.Categories on tran.CategoryId equals cat.CategoryId
                               into temp
                               from category in temp.DefaultIfEmpty()
                               join subCat in db.SubCategories on tran.SubCategoryId equals subCat.SubCategoryId
                               into temp2
                               from subcategory in temp2.DefaultIfEmpty()
                               join am in db.MoneyAccounts on tran.MoneyAccountId equals am.MoneyAccountId
                               into temp3
                               from moneyaccount in temp3.DefaultIfEmpty()
                               where tran.UserId==userId
                               orderby tran.TransactionDate
                               select (new TransactionVM() {
                                   UserId=tran.UserId,
                                   MoneyAccountId=tran.MoneyAccountId,
                                   CategoryId=tran.CategoryId,
                                   SubCategoryId=tran.SubCategoryId,
                                   TransactionDate=tran.TransactionDate,
                                   Description=tran.Description,
                                   TransactionId=tran.TransactionId,
                                   TransactionType=tran.TransactionType,
                                   Amount=tran.Amount,
                                   TransferTo = tran.TransferTo,
                                   CategoryName=category.Name,
                                   CategoryIcon = category.IconId,
                                   CategoryColor = category.Color,
                                   SubCategoryName =subcategory.Name,
                                   SubCategoryIcon = subcategory.IconId,
                                   SubCategoryColor=subcategory.Color,
                                   MoneyAccountName =moneyaccount.Name,
                               });
            if (period.Type != PeriodType.All)
                transactions = transactions.Where(t => t.TransactionDate >= period.FromDate && t.TransactionDate <= period.ToDate);
            return transactions;
        }
        public IQueryable<TransactionVM> GetTransactionsVM(string userId, Period period,int account)
        {
            if (account == AccountType.All)
                return GetTransactionsVM(userId, period);
            else
                return GetTransactionsVM(userId, period)
                    .Where(c => c.MoneyAccountId == account);
        }

        public IQueryable<CategoryGroupByVM> GetTransactionsGroupByCategory(string userId, Period period, int account,TransactionType transactionType)
        {

            IQueryable<TransactionVM> transaction;


            if (account == AccountType.All)
                transaction = GetTransactionsVM(userId, period)
                    .Where(c => c.TransactionType == transactionType);
            else
                transaction = GetTransactionsVM(userId, period)
                    .Where(c => c.MoneyAccountId == account
                            && c.TransactionType == transactionType);

            var trans = transaction
                        .GroupBy(p => p.CategoryName)
                        .Select(cl => new CategoryGroupByVM
                        {
                            Name = cl.FirstOrDefault().CategoryName,
                            Color = cl.FirstOrDefault().CategoryColor,
                            IconId = cl.FirstOrDefault().CategoryIcon,
                            Sum = cl.Sum(a => a.Amount)
                        })
                        .OrderBy(o => o.Sum);

            return trans;
        }

        public TransactionVM GetTransactionVM(int transactionId)
        {
            var transactions = from tran in db.Transactions
                               join cat in db.Categories on tran.CategoryId equals cat.CategoryId
                               into temp
                               from category in temp.DefaultIfEmpty()
                               join subCat in db.SubCategories on tran.SubCategoryId equals subCat.SubCategoryId
                               into temp2
                               from subcategory in temp2.DefaultIfEmpty()
                               join am in db.MoneyAccounts on tran.MoneyAccountId equals am.MoneyAccountId
                               into temp3
                               from moneyaccount in temp3.DefaultIfEmpty()
                               where tran.TransactionId == transactionId
                               orderby tran.TransactionDate
                               select (new TransactionVM()
                               {
                                   UserId = tran.UserId,
                                   MoneyAccountId = tran.MoneyAccountId,
                                   CategoryId = tran.CategoryId,
                                   SubCategoryId = tran.SubCategoryId,
                                   TransactionDate = tran.TransactionDate,
                                   Description = tran.Description,
                                   TransactionId = tran.TransactionId,
                                   TransactionType = tran.TransactionType,
                                   Amount = tran.Amount,
                                   TransferTo = tran.TransferTo,
                                   CategoryName = category.Name,
                                   CategoryIcon = category.IconId,
                                   CategoryColor = category.Color,
                                   SubCategoryName = subcategory.Name,
                                   SubCategoryIcon = subcategory.IconId,
                                   SubCategoryColor = subcategory.Color,
                                   MoneyAccountName = moneyaccount.Name,
                               });

            return transactions.FirstOrDefault();
        }

        public void Update(Transaction transaction)
        {
            try
            {
                var old_transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transaction.TransactionId);
                db.Entry(old_transaction).CurrentValues.SetValues(transaction);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.UPDATE_ITEM, ex, "Update transaction");
            }
        }


        #region balance

        private IQueryable<Transaction> GetBalanceBase(string userId, int moneyAccountId=AccountType.All, Period period=null)
        {
            var balance= db.Transactions
                    .Where(t => t.UserId == userId);

            if (moneyAccountId != AccountType.All)
                balance = balance.Where(x => x.MoneyAccountId == moneyAccountId);

            if (period!=null)
                if (period.Type != PeriodType.All)
                    balance = balance.Where(t => t.TransactionDate >= period.FromDate 
                                                && t.TransactionDate <= period.ToDate);

            return balance;
        }

        public decimal GetBalance(string userId, int moneyAccountId = AccountType.All, Period period = null)
        {
            var income = GetBalance(userId, moneyAccountId, period, TransactionType.Income);

            var expense = GetBalance(userId, moneyAccountId, period, TransactionType.Expense);
            return (income-expense);
        }

        public decimal GetBalance(string userId, int moneyAccountId, Period period,TransactionType transactionType)
        {
            var balance = GetBalanceBase(userId, moneyAccountId, period)
                            .Where(x => x.TransactionType == transactionType)
                            .Sum(s => (decimal?)s.Amount) ?? 0;
            return balance;
        }

        public void Insert(Transaction transaction)
        {
            try
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.INSERT_ITEM, ex, "Insert transaction in database");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var item = GetById(id);
                if (item != null)
                {
                    db.Transactions.Remove(item);
                    db.SaveChanges();
                }
                else
                {
                    logger.LogError(LoggingEvents.GET_ITEM_NOTFOUND, "Item couldn't be found");
                }
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.DELETE_ITEM, ex, "Delete transaction from database");
            }
        }

        public Transaction GetById(int id)
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction = db.Transactions.FirstOrDefault(x=>x.TransactionId==id);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, "Get transaction from database");
            }
            return transaction;
        }


        #endregion


    }
}
