using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Icon
    {
        [Required]
        public int IconId { get; set; }
        [Required]
        public string Description { get; set; }
        public string LocalPath { get; set; }
        public string AwesomeFont { get; set; }
        public string WebHostingHubFont { get; set; }
    }
}
