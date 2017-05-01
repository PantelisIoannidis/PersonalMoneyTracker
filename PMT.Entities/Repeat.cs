using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Repeat
    {
        public int RepeatId { get; set; }
        public string Description { get; set; }
        public int AddDays { get; set; }
        public int AddWeeks { get; set; }
        public int AddMonths { get; set; }
        public int AddYears { get; set; }
    }
}
