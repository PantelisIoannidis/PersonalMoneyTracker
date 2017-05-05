using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Icon
    {
        [Key]
        public string IconId { get; set; }
        [Display(Name = nameof(ModelText.IconName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

        public bool IsFontAwesome { get; set; }
        public bool IsWebHostingHubGlyphs { get; set; }
    }
}
