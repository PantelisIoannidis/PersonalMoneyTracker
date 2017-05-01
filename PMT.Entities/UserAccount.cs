using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
