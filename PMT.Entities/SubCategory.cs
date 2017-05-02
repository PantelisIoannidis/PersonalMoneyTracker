using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int IconId { get; set; }
        public int ColorId { get; set; }
        [Display(Name = nameof(ModelText.SubCategoryName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

    }
}
