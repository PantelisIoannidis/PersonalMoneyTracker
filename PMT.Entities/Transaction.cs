using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Transaction
    {
        private decimal _amount;

        public int TransactionId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
        [ForeignKey("MoneyAccount")]
        public int MoneyAccountId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        [Display(Name = nameof(ModelText.TransactionTransactionType), ResourceType = typeof(ModelText))]
        public TransactionType TransactionType { get; set; }

        [Display(Name = nameof(ModelText.TransactionDate), ResourceType = typeof(ModelText))]
        public DateTime TransactionDate { get; set; }

        [Display(Name = nameof(ModelText.TransactionDescription), ResourceType = typeof(ModelText))]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = nameof(ModelText.TransactionAmount), ResourceType = typeof(ModelText))]
        public decimal Amount {
            get { return _amount; }
            set
            {
                if (TransactionType == TransactionType.Income || TransactionType == TransactionType.Transfer)
                    _amount = Math.Abs(value);
                else
                    if (TransactionType == TransactionType.Expense)
                    _amount = Math.Abs(value) * (-1);
                else
                    _amount = value;
            }
        }


        public int MoveToAccount { get; set; }

        public MoneyAccount MoneyAccount { get; set; }
        
    }
}
