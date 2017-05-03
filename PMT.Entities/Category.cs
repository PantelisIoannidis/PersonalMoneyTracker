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
    public class Category
    {

        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
        
        public int CategoryId { get; set; }

        public int IconId { get; set; }
        [Display(Name = nameof(ModelText.CategoryType), ResourceType = typeof(ModelText))]
        public TransactionType Type { get; set; }
        [Display(Name = nameof(ModelText.CategoryName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
