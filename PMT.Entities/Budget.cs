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
        public int UserAccountId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        public int RepeatId { get; set; }
        [Display(Name="Move Forward")]
        public bool MoveForward { get; set; }

    }
}
