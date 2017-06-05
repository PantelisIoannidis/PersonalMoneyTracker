using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public interface ISubCategoryRepository : IRepositoryBase<SubCategory>
    {
        List<SubCategory> GetSubCategories(string userId, int CategoryId);
        SubCategory GetSubCategoryById(string userId, int SubCategoryId);
        void StoreSubCategory(SubCategory subCategory);
        void UpdateSubCategory(CategoryVM categoryVM);
    }
}
