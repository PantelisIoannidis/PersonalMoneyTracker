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
    public class SubCategoryRepository : RepositoryBase<SubCategory> ,ISubCategoryRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        public SubCategoryRepository(ILoggerFactory logger, IActionStatus actionStatus)
            : base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<CategoryRepository>();
        }

        public List<SubCategory> GetSubCategories(string userId,int CategoryId)
        {
            return db.SubCategories.OrderBy(o => o.Name).Where(w => w.CategoryId == CategoryId && w.UserId==userId).ToList();
        }

        public SubCategory GetSubCategoryById(string userId, int SubCategoryId)
        {
            return db.SubCategories.FirstOrDefault(w => w.SubCategoryId == SubCategoryId && w.UserId == userId);
        }

        public void StoreSubCategory(SubCategory subCategory)
        {
            try { 
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.INSERT_ITEM, ex, "Store subcategory in database");
            }
        }

        public void UpdateSubCategory(CategoryVM categoryVM)
        {
            try
            {
                var subcategory = db.SubCategories.FirstOrDefault(w => w.SubCategoryId == categoryVM.SubCategoryId);
                subcategory.Color = categoryVM.Color;
                subcategory.IconId = categoryVM.IconId;
                subcategory.Name = categoryVM.Name;
                subcategory.CategoryId = categoryVM.CategoryId;
                subcategory.UserId = categoryVM.UserId;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.UPDATE_ITEM, ex, "Update subcategory in database");
            }
        }
    }
}
