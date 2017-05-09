﻿using PMT.Common.Helpers;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository()
            : base(new MainDb())
        {
        }

        public IQueryable<TransactionVM> GetTransactions(string userId,TimeDuration timeDuration)
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

            return transactions;
        }
        public IQueryable<TransactionVM> GetTransactions(string userId, TimeDuration timeDuration,int account)
        {
            return GetTransactions(userId, timeDuration)
                    .Where(c => c.MoneyAccountId == account);
        }


            #region balance
            public decimal GetBalance(string userId)
        {
            return db.Transactions
                    .Where(t=>t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }
        public decimal GetBalancePerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.UserId == userId && t.MoneyAccountId==moneyAccountId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetIncome(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Income && t.UserId==userId)
                    .Sum(s => (decimal?)s.Amount)??0;
        }

        public decimal GetAdjustment(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetExpense(string userId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetIncomePerAccount(string userId,int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Income && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetAdjustmentPerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }

        public decimal GetExpensePerAccount(string userId, int moneyAccountId)
        {
            return db.Transactions
                    .Where(t => t.TransactionType == TransactionType.Adjustment && t.UserId == userId)
                    .Sum(s => (decimal?)s.Amount) ?? 0;
        }
#endregion


    }
}
