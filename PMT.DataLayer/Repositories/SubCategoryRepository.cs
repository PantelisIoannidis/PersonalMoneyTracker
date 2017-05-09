using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT.Contracts.Repositories;

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

        public List<SubCategory> GetSubCategory(int CategoryId)
        {
            return db.SubCategories.OrderBy(o => o.Name).Where(w => w.CategoryId == CategoryId).ToList();
        }
    }
}
