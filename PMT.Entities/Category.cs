using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class Category
    {
        //[HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        public int IconId { get; set; }
        [Display(Name = nameof(ModelText.CategoryType), ResourceType = typeof(ModelText))]
        public TransactionType Type { get; set; }
        [Display(Name = nameof(ModelText.CategoryName), ResourceType = typeof(ModelText))]
        public string Name { get; set; }

    }
}
