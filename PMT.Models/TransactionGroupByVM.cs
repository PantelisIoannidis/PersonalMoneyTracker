using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionGroupByVM
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string IconId { get; set; }
        public decimal Sum { get; set;}
    }
}
