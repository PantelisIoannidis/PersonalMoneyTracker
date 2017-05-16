using PMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class PaginationVM
    {
        public Pager pager { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
