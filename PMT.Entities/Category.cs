using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
