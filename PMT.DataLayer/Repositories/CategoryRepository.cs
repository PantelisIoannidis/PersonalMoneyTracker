using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Contracts.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        public CategoryRepository(ILoggerFactory logger, IActionStatus actionStatus) 
            :base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<CategoryRepository>();
        }

        public List<Category> GetGategory(TransactionType transactionType)
        {
            return db.Categories.OrderBy(o => o.Name).Where(w => w.Type == transactionType).ToList();
        }

        public List<Category> GetAllGategoriesSubCategories()
        {
            return db.Categories.Include("SubCategories").OrderBy(x=>x.Name).ToList();
        }
    }
}
