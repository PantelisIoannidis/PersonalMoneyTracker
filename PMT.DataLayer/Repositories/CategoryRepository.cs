using PMT.Contracts.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        MainDb db;
        public CategoryRepository() 
        {
            db = new MainDb();
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }
    }
}
