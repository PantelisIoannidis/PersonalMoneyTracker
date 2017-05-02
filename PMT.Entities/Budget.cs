using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    
    public class Budget
    {
        public int BudgetId { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public int MoneyAccountId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = nameof(ModelText.BudgetAmount), ResourceType = typeof(ModelText))]
        public decimal Amount { get; set; }
        [Display(Name = nameof(ModelText.BudgetFromDate), ResourceType = typeof(ModelText))]
        public DateTime FromDate { get; set; }
        public int RepeatId { get; set; }
        [Display(Name = nameof(ModelText.BudgetMoveForward), ResourceType = typeof(ModelText))]
        public bool MoveForward { get; set; }

    }
}
