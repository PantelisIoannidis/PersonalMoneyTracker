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
        [Required]
        public int UserAccountId { get; set; }
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
