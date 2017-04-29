using System.Collections.Generic;
using PMT.Entities;

namespace PMT.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}