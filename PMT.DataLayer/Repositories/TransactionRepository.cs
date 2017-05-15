using PMT.Common.Helpers;
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

        public IQueryable<TransactionVM> GetTransactionsVM(string userId,Period timeDuration)
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
            if (timeDuration.Type != PeriodType.All)
                transactions = transactions.Where(t => t.TransactionDate >= timeDuration.FromDate && t.TransactionDate <= timeDuration.ToDate);
            return transactions;
        }
        public IQueryable<TransactionVM> GetTransactionsVM(string userId, Period timeDuration,int account)
        {
            if (account < 0)
                return GetTransactionsVM(userId, timeDuration);
            else
                return GetTransactionsVM(userId, timeDuration)
                    .Where(c => c.MoneyAccountId == account);
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

            }
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
