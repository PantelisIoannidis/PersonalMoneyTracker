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
using static PMT.Entities.Literals;

namespace PMT.BusinessLayer
{

    public class CategoriesEngine : BaseEngine, ICategoriesEngine
    {
        ILogger logger;
        IActionStatus actionStatus;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        

        public CategoriesEngine(ILoggerFactory logger,
            IActionStatus actionStatus,
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository)
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<CategoriesEngine>();
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public CategoryVM GetCategory(string userId, string id)
        {
            CategoryVM categoryVM = new CategoryVM();

            try
            {
                string categoryId = "";
                string subCategoryId = "";
                if (id.Contains(Literals.MiscMagicStrings.CategoryIdPrefix))
                    categoryId = id.Replace(Literals.MiscMagicStrings.CategoryIdPrefix, "");
                if (id.Contains(Literals.MiscMagicStrings.SubcategoryIdPrefix))
                    subCategoryId = id.Replace(Literals.MiscMagicStrings.SubcategoryIdPrefix, "");

                if (!string.IsNullOrEmpty(subCategoryId))
                {
                    var subCategory = subCategoryRepository.GetSubCategoryById(userId, subCategoryId.ParseInt());
                    categoryVM.CategoryId = subCategory.CategoryId;
                    categoryVM.SubCategoryId = subCategory.SubCategoryId;
                    categoryVM.IconId = subCategory.IconId;
                    categoryVM.Name = subCategory.Name;
                    categoryVM.Color = subCategory.Color;
                    categoryVM.UserId = subCategory.UserId;
                    var category = categoryRepository.GetGategoryById(userId,subCategory.CategoryId);
                    categoryVM.Type = category.Type;
                    categoryVM.IsCategory = false;

                }
                else if (!string.IsNullOrEmpty(categoryId))
                {
                    var category = categoryRepository.GetGategoryById(userId,categoryId.ParseInt());
                    categoryVM.CategoryId = category.CategoryId;
                    categoryVM.IconId = category.IconId;
                    categoryVM.Name = category.Name;
                    categoryVM.Color = category.Color;
                    categoryVM.Type = category.Type;
                    categoryVM.UserId = category.UserId;
                    categoryVM.IsCategory = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.POPULATE_OBJECT, ex, "Populate CategoryViewModel from Category or Subcategory table");
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
                    Type = categoryVM.Type,
                    UserId=categoryVM.UserId
                };
                var subCategory = new SubCategory()
                {
                    Name = categoryVM.Name,
                    IconId = categoryVM.IconId,
                    Color = categoryVM.Color,
                    UserId = categoryVM.UserId
                };
                categoryRepository.StoreNewCategoryAndSubCategory(category, subCategory);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository to store new category");
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
                    Color = categoryVM.Color,
                    UserId = categoryVM.UserId
                };
                subCategoryRepository.StoreSubCategory(subCategory);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository to store new subcategory");
            }
        }

        public ActionStatus DeleteCategorySubCategories(string userId, string id)
        {
            try
            {
                string categoryId = "";
                string subCategoryId = "";
                if (id.Contains(Literals.MiscMagicStrings.CategoryIdPrefix))
                    categoryId = id.Replace(Literals.MiscMagicStrings.CategoryIdPrefix, "");
                if (id.Contains(Literals.MiscMagicStrings.SubcategoryIdPrefix))
                    subCategoryId = id.Replace(Literals.MiscMagicStrings.SubcategoryIdPrefix, "");

                CategoryVM categoryVM = new CategoryVM();

                if (!string.IsNullOrEmpty(subCategoryId))
                {
                    var subCategory = subCategoryRepository.GetSubCategoryById(userId,subCategoryId.ParseInt());
                    if (subCategory != null)
                    {
                        subCategoryRepository.Delete(subCategory);
                    }else
                    {
                        actionStatus = ActionStatus.CreateFromConditions("Item not found");
                    }

                }
                else if (!string.IsNullOrEmpty(categoryId))
                {
                    var category = categoryRepository.GetGategoryById(userId, categoryId.ParseInt());
                    if (category != null)
                    {
                        categoryRepository.Delete(category);
                    }
                    else
                    {
                        actionStatus = ActionStatus.CreateFromConditions("Item not found");
                    }
                }
            }
            catch (Exception ex)
            {
                var message = "Delete a category or subcategory";
                actionStatus = ActionStatus.CreateFromException(message, ex);
                logger.LogError(LoggingEvents.DELETE_ITEM, ex, message);
            }
            return (ActionStatus)actionStatus;
        }

        public void EditCategoryAndSubCategory(CategoryVM categoryVM)
        {
            try
            {

                if (categoryVM.IsCategory)
                {
                    categoryRepository.UpdateCategory(categoryVM);

                }
                else
                {
                    subCategoryRepository.UpdateSubCategory(categoryVM);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.CALL_METHOD, ex, "Call repository to edit category or subcategory");
            }
        }

        public IEnumerable<Category> GetAllGategoriesSubCategories(string userId)
        {
            return categoryRepository.GetAllGategoriesSubCategories(userId)
                .Where(x => string.IsNullOrEmpty(x.SpecialAttribute));
        }

        public IEnumerable<Category> GetAllSpecialGategoriesSubCategories(string userId)
        {
            return categoryRepository.GetAllGategoriesSubCategories(userId)
                .Where(x => !string.IsNullOrEmpty(x.SpecialAttribute));
        }

        public IEnumerable<Category> GetCategories(string userId, TransactionType type)
        {
            return categoryRepository.GetGategories(userId, type).Where(x => string.IsNullOrEmpty(x.SpecialAttribute));
        }

        public IEnumerable<SubCategory> GetSubCategories(string userId, int categoryId)
        {
            return subCategoryRepository.GetSubCategories(userId, categoryId).Where(x => string.IsNullOrEmpty(x.SpecialAttribute));
        }


    }
}
