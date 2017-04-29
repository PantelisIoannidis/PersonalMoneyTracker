﻿using PMT.Contracts.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        
        public CategoryRepository() 
            :base(new MainDb())
        {
            
        }

    }
}
