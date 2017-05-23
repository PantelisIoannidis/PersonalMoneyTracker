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

        public List<SubCategory> GetSubCategories(int CategoryId)
        {
            return db.SubCategories.OrderBy(o => o.Name).Where(w => w.CategoryId == CategoryId).ToList();
        }

        public SubCategory GetSubCategoryById(int SubCategoryId)
        {
            return db.SubCategories.FirstOrDefault(w => w.SubCategoryId == SubCategoryId);
        }

        public void StoreSubCategory(SubCategory subCategory)
        {
            db.SubCategories.Add(subCategory);
            db.SaveChanges();
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
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Update subcategory in database");
            }
        }
    }
}
