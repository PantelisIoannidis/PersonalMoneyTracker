using PMT.Common;
using PMT.Models;

namespace PMT.BusinessLayer
{
    public interface ICategoriesEngine
    {
        CategoryVM GetCategory(string id);
        IActionStatus StoreCategory(CategoryVM categoryVM);
    }
}