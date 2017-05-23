using System.Collections.Generic;
using PMT.Entities;
using PMT.Common;
using PMT.Models;

namespace PMT.DataLayer.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        List<Category> GetGategory(TransactionType transactionType);
        List<Category> GetAllGategoriesSubCategories();
        Category GetGategoryById(int id);

        void StoreNewCategoryAndSubCategory(Category category, SubCategory subCategory);

        void UpdateCategory(CategoryVM categoryVM);
    }
}