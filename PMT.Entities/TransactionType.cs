using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public enum TransactionType
    {
        [Display(Name = nameof(ModelText.TransactionTypeIncome), ResourceType = typeof(ModelText))]
        Income = 0,

        [Display(Name = nameof(ModelText.TransactionTypeExpense), ResourceType = typeof(ModelText))]
        Expense = 1,

        [Display(Name = nameof(ModelText.TransactionTypeTransfer), ResourceType = typeof(ModelText))]
        Transfer = 2,

        [Display(Name = nameof(ModelText.TransactionTypeAdjustment), ResourceType = typeof(ModelText))]
        Adjustment = 3
    }
}
