using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMT.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public string IconId { get; set; }
        public string Color { get; set; }

        public TransactionType Type { get; set; }

        public string Name { get; set; }

    }
}