using PMT.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class MoneyAccountVM : MoneyAccount
    {
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
