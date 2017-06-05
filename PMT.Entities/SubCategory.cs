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

        
        [ForeignKey("Category"), Column(Order = 0)]
        public int CategoryId { get; set; }
        [Key,ForeignKey("Category"), Column(Order = 1)]
        public string UserId { get; set; }
        [Key, Column(Order = 2)]
        public int SubCategoryId { get; set; }
        public string IconId { get; set; }
        public string Color { get; set; }
        [Display(Name = nameof(ModelText.SubCategoryName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

        public string SpecialAttribute { get; set; }
        public Category Category { get; set; }

    }
}
