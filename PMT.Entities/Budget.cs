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
        [Required]
        public int BudgetId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int UserAccountId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        public int RepeatId { get; set; }

        [Required]
        [Display(Name="Move Forward")]
        public bool MoveForward { get; set; }

    }
}
