using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionsSummaryVM
    {
        [DataType(DataType.Currency)]
        public decimal Income { get; set; }

        [DataType(DataType.Currency)]
        public decimal Expenses { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
