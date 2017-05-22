using PMT.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMT.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }
        [Required]
        public string IconId { get; set; }
        public string Color { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        public string Name { get; set; }

    }
}