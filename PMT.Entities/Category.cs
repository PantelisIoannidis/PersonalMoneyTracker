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

        public int Type { get; set; }
        public string Name { get; set; }

    }
}
