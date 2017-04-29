﻿using System;
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
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
