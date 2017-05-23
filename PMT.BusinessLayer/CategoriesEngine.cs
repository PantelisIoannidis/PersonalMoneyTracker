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
            IActionStatus actionStatux,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository)
        {
            this.actionStatus = actionStatux;
            this.logger = logger.CreateLogger<CategoriesEngine>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public bool IsCategoryNotSubCategory(string id)
        {
            return id.Contains("categoryId_") ? true : false;
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
                categoryVM.IsCategory = false;

            }
            else if (!string.IsNullOrEmpty(categoryId))
            {
                var category = categoryRepository.GetGategoryById(categoryId.ParseInt());
                categoryVM.CategoryId = category.CategoryId;
                categoryVM.IconId = category.IconId;
                categoryVM.Name = category.Name;
                categoryVM.Color = category.Color;
                categoryVM.Type = category.Type;
                categoryVM.IsCategory = true;
            }
            return categoryVM;
        }

        public void StoreNewCategoryAndSubCategory(CategoryVM categoryVM)
        {
            try
            {
                var category = new Category()
                {
                    CategoryId = categoryVM.CategoryId,
                    Color = categoryVM.Color,
                    IconId = categoryVM.IconId,
                    Name = categoryVM.Name,
                    Type = categoryVM.Type
                };
                var subCategory = new SubCategory()
                {
                    Name = categoryVM.Name,
                    IconId = categoryVM.IconId,
                    Color = categoryVM.Color
                };
                categoryRepository.StoreNewCategoryAndSubCategory(category, subCategory);



            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Preparing to store new category didn't work");
            }

        }

        public void StoreNewSubCategory(CategoryVM categoryVM)
        {
            try
            {
                var subCategory = new SubCategory()
                {
                    CategoryId = categoryVM.CategoryId,
                    Name = categoryVM.Name,
                    IconId = categoryVM.IconId,
                    Color = categoryVM.Color
                };
                subCategoryRepository.StoreSubCategory(subCategory);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Preparing to store new subcategory didn't work");
            }

        }

        public void DeleteCategorySubCategories(string id)
        {
            try
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
                    subCategoryRepository.Delete(subCategory);
                    subCategoryRepository.Save();

                }
                else if (!string.IsNullOrEmpty(categoryId))
                {
                    var category = categoryRepository.GetGategoryById(categoryId.ParseInt());
                    categoryRepository.Delete(category);
                    categoryRepository.Save();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Preparing to delete a category or subcategory didn't work");
            }

        }

        public void EditCategoryAndSubCategory(CategoryVM categoryVM)
        {
            try
            {

                if (categoryVM.IsCategory)
                {
                    categoryRepository.UpdateCategory(categoryVM);

                }else
                {
                    subCategoryRepository.UpdateSubCategory(categoryVM);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Preparing to edit category,subcategory didn't work");
            }

        }
    }
}
