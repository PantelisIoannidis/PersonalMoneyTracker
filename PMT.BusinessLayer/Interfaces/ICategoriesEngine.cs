using PMT.Common;
using PMT.Entities;
using PMT.Models;
using System.Collections.Generic;

namespace PMT.BusinessLayer
{
    public interface ICategoriesEngine
    {
        CategoryVM GetCategory(string id);
        void StoreNewCategoryAndSubCategory(CategoryVM categoryVM);
        void StoreNewSubCategory(CategoryVM categoryVM);
        IEnumerable<Category> GetAllGategoriesSubCategories();
        ActionStatus DeleteCategorySubCategories(string id);
        void EditCategoryAndSubCategory(CategoryVM categoryVM);
    }
}