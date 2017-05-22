using System.Collections.Generic;
using PMT.Entities;

namespace PMT.DataLayer.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        List<Category> GetGategory(TransactionType transactionType);
        List<Category> GetAllGategoriesSubCategories();
        Category GetGategoryById(int id);
    }
}