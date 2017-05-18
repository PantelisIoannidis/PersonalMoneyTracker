using System.Collections.Generic;
using PMT.Entities;

namespace PMT.Contracts.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        List<Category> GetGategory(TransactionType transactionType);
        List<Category> GetAllGategoriesSubCategories();
    }
}