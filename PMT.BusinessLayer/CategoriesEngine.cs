using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{

    public class CategoriesEngine : ICategoriesEngine
    {
        ILogger logger;
        IActionStatus actionStatus;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;

        public CategoriesEngine(ILoggerFactory logger,
            IActionStatus operationStatus,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository)
        {
            this.actionStatus = operationStatus;
            this.logger = logger.CreateLogger<CategoriesEngine>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public CategoryVM GetCategory(string id)
        {
            string categoryId = "";
            string subCategoryId = "";
            if (id.Contains("categoryId_"))
                categoryId = id.Replace("categoryId_", "");
            if (id.Contains("subCategoryId_"))
                subCategoryId = id.Replace("subCategoryId_", "");

            CategoryVM categoryVM = new CategoryVM();

            if (!string.IsNullOrEmpty(subCategoryId))
            {
                var subCategory = subCategoryRepository.GetSubCategoryById(subCategoryId.ParseInt());
                categoryVM.CategoryId = subCategory.CategoryId;
                categoryVM.SubCategoryId = subCategory.SubCategoryId;
                categoryVM.IconId = subCategory.IconId;
                categoryVM.Name = subCategory.Name;
                categoryVM.Color = subCategory.Color;
                var category = categoryRepository.GetById(subCategory.CategoryId);
                categoryVM.Type = category.Type;

            }
            else if (!string.IsNullOrEmpty(categoryId))
            {
                var category = categoryRepository.GetGategoryById(categoryId.ParseInt());
                categoryVM.CategoryId = category.CategoryId;
                categoryVM.IconId = category.IconId;
                categoryVM.Name = category.Name;
                categoryVM.Color = category.Color;
                categoryVM.Type = category.Type;
            }
                return categoryVM;
        }

        public IActionStatus StoreCategory(CategoryVM categoryVM)
        {
            try
            {
                var category = new Category() {
                    CategoryId=categoryVM.CategoryId,
                    Color=categoryVM.Color,
                    IconId=categoryVM.IconId,
                    Name=categoryVM.Name,
                    Type=categoryVM.Type
                };
                actionStatus = categoryRepository.StoreCategory(category);

            }
            catch (Exception ex)
            {
                actionStatus = ActionStatus.CreateFromException("", ex);
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Preparing to store new category didn't work");
            }

            return actionStatus;

        }

    }
}
