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
    public class CategoryRepository : ICategoryRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        MainDb db;
        public CategoryRepository(MainDb db,ILoggerFactory logger, IActionStatus actionStatus)
        {
            this.db = db;
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<CategoryRepository>();
        }

        public IEnumerable<Category> GetGategories(string userId,TransactionType transactionType)
        {
            return db.Categories.OrderBy(o => o.Name).Where(w => w.Type == transactionType && w.UserId == userId);
        }

        public IEnumerable<Category> GetAllGategoriesSubCategories(string userId)
        {
            return db.Categories.Include("SubCategories").OrderBy(x => x.Name).Where(w=>w.UserId==userId);
        }

        public Category GetGategoryById(string userId, int id)
        {
            return db.Categories.FirstOrDefault(w => w.CategoryId == id && w.UserId == userId);
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
                category.UserId = categoryVM.UserId;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.UPDATE_ITEM, ex, "Update category in database");
            }
        }

        public void Delete(Category category)
        {
            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.DELETE_ITEM, ex, "Delete category from database");
            }
        }
    }
}
