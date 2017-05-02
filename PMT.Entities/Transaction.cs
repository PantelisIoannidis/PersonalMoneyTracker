using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public int MoneyAccountId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        [Display(Name = nameof(ModelText.TransactionTransactionType), ResourceType = typeof(ModelText))]
        public TransactionType TransactionType { get; set; }
        [Display(Name = nameof(ModelText.TransactionDate),ResourceType =typeof(ModelText))]
        public DateTime TransactionDate { get; set; }
        [Display(Name =nameof(ModelText.TransactionDescription),ResourceType =typeof(ModelText))]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name =nameof(ModelText.TransactionAmount),ResourceType =typeof(ModelText))]
        public decimal Amount { get; set; }
        public int MoveToAccount { get; set; }

        
    }
}
