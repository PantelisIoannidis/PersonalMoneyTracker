using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionsFilterPreferences
    {
        
        public int AccountFilterId { get; set; }
        public int PeriodFilterId { get; set; }
        public string SelectedDateFull { get; set; }
        public string Operation { get; set; }
    }
}
