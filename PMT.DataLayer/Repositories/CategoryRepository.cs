using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Entities;
using PMT.Models;
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
            : base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<CategoryRepository>();
        }

        public List<Category> GetGategories(TransactionType transactionType)
        {
            return db.Categories.OrderBy(o => o.Name).Where(w => w.Type == transactionType).ToList();
        }

        public List<Category> GetAllGategoriesSubCategories()
        {
            return db.Categories.Include("SubCategories").OrderBy(x => x.Name).ToList();
        }

        public Category GetGategoryById(int id)
        {
            return db.Categories.FirstOrDefault(w => w.CategoryId == id);
        }

        public void StoreNewCategoryAndSubCategory(Category category, SubCategory subCategory)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    subCategory.CategoryId = category.CategoryId;
                    db.SubCategories.Add(subCategory);
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    logger.LogError(LoggingEvents.INSERT_ITEM, ex, "Store new category to database");
                }
            }
        }

        public void UpdateCategory(CategoryVM categoryVM)
        {
            try
            {
                var category=db.Categories.FirstOrDefault(w => w.CategoryId == categoryVM.CategoryId);
                category.Color = categoryVM.Color;
                category.IconId = categoryVM.IconId;
                category.Name = categoryVM.Name;
                category.Type = categoryVM.Type;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.UPDATE_ITEM, ex, "Update category in database");
            }
        }
    }
}
