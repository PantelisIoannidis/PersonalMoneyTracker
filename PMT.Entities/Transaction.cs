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
        public int UserAccountId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int MoveToAccount { get; set; }

        
    }
}
