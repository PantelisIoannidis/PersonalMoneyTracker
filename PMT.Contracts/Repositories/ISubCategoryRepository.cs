using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Contracts.Repositories
{
    public interface ISubCategoryRepository : IRepositoryBase<SubCategory>
    {
        List<SubCategory> GetSubCategories(int CategoryId);
        SubCategory GetSubCategoryById(int CategoryId);
    }
}
