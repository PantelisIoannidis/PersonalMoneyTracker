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
    }
}
