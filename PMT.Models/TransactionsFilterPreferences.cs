using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class TransactionsFilterPreferences
    {
        public DateTime SelectedDate { get; set; }
        public int PeriodCategory { get; set; }
        public int AccountFilterId { get; set; }
        public int PeriodFilterId { get; set; }
    }
}
