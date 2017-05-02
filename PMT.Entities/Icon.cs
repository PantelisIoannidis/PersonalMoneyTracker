using PMT.Common.Resources;
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
        public int IconId { get; set; }
        [Display(Name = nameof(ModelText.IconName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }
        public string LocalPath { get; set; }
        public string AwesomeFont { get; set; }
        public string WebHostingHubFont { get; set; }

    }
}
