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
        [Required]
        public int TransactionId { get; set; }
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public int TransactionType { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public int MoveToAccount { get; set; }
    }
}
