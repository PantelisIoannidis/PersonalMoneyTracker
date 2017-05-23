using PMT.Common;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface ICategoriesEngine
    {
        CategoryVM GetCategory(string id);
        void StoreNewCategoryAndSubCategory(CategoryVM categoryVM);
        void StoreNewSubCategory(CategoryVM categoryVM);

        void DeleteCategorySubCategories(string id);
        bool IsCategoryNotSubCategory(string id);
        void EditCategoryAndSubCategory(CategoryVM categoryVM);
    }
}