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
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string IconId { get; set; }
        public string Color { get; set; }
        [Display(Name = nameof(ModelText.SubCategoryName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }


        public Category Category { get; set; }

    }
}
