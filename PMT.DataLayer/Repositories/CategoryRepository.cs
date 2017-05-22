using Microsoft.Extensions.Logging;
using PMT.Common;
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

        public Category GetGategoryById(int id)
        {
            return db.Categories.FirstOrDefault(w => w.CategoryId == id);
        }

        public IActionStatus StoreCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                actionStatus = ActionStatus.CreateFromException("", ex);
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Store new category to database didn't work");
            }

            return actionStatus;
        }
    }
}
