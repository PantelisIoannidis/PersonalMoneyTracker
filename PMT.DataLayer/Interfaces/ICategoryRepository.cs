﻿using System.Collections.Generic;
using PMT.Entities;
using PMT.Common;
using PMT.Models;

namespace PMT.DataLayer.Repositories
{
    public interface ICategoryRepository 
    {
        IEnumerable<Category> GetGategories(string userId, TransactionType transactionType);
        IEnumerable<Category> GetAllGategoriesSubCategories(string userId);
        Category GetGategoryById(string userId, int id);
        Category GetGategoryByName(string userId, string name);
        void StoreNewCategoryAndSubCategory(Category category, SubCategory subCategory);

        void UpdateCategory(CategoryVM categoryVM);
        void Delete(Category category);
    }
}