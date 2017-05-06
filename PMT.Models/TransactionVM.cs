using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionVM : Transaction
    {
        public string MoneyAccountName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public string SubCategoryColor { get; set; }
    }
}
