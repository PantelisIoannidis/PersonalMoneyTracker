using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class Mapping : IMapping
    {
        public TransactionVM TransactionToTransactionVM(Transaction source)
        {
            return new TransactionVM() {
                UserId = source.UserId,
                TransactionId = source.TransactionId,
                TransactionType=source.TransactionType,
                CategoryId = source.CategoryId,
                SubCategoryId = source.SubCategoryId,
                MoneyAccountId = source.MoneyAccountId,
                Amount = source.Amount,
                Description = source.Description,
                TransferTo = source.TransferTo,
                TransactionDate=source.TransactionDate,
            };
        }

        public Transaction TransactionVMToTransaction(TransactionVM source)
        {
            return new Transaction()
            {
                UserId = source.UserId,
                TransactionId = source.TransactionId,
                TransactionType = source.TransactionType,
                CategoryId = source.CategoryId,
                SubCategoryId = source.SubCategoryId,
                MoneyAccountId = source.MoneyAccountId,
                Amount = source.Amount,
                Description = source.Description,
                TransferTo = source.TransferTo,
                TransactionDate = source.TransactionDate,
            };
        }

    }
}
