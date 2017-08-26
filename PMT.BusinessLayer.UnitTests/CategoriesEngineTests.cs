using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using PMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.UnitTests
{
    [TestFixture]
    public class CategoriesEngineTests
    {
        private Mock<ICategoryRepository> mockCategoryRepository;
        private Mock<ISubCategoryRepository> mockSubCategoryRepository;
        private Mock<ILoggerFactory> mockLogger;
        IActionStatus actionStatus;
        private List<Category> categoryListFromDatabase;
        private List<SubCategory> subCategoryListFromDatabase;

        [SetUp]
        public void Setup()
        {
            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
            mockLogger = new Mock<ILoggerFactory>();
            actionStatus = new ActionStatus();
            categoryListFromDatabase = new List<Category>() {
                new Category(){ CategoryId=1,SpecialAttribute=Literals.SpecialAttributes.AdjustmentIncome,Type=TransactionType.Adjustment},
                new Category(){ CategoryId=2,SpecialAttribute=null,Type=TransactionType.Expense},
                new Category(){ CategoryId=3,SpecialAttribute=null,Type=TransactionType.Income},
                new Category(){ CategoryId=4,SpecialAttribute=Literals.SpecialAttributes.TransferExpense,Type=TransactionType.Transfer},
                new Category(){ CategoryId=5,SpecialAttribute=null,Type=TransactionType.Expense},
            };
            subCategoryListFromDatabase = new List<SubCategory>() {
                new SubCategory(){ CategoryId=1,SpecialAttribute=Literals.SpecialAttributes.AdjustmentIncome},
                new SubCategory(){ CategoryId=2,SpecialAttribute=null},
                new SubCategory(){ CategoryId=3,SpecialAttribute=null},
                new SubCategory(){ CategoryId=4,SpecialAttribute=Literals.SpecialAttributes.TransferExpense},
                new SubCategory(){ CategoryId=5,SpecialAttribute=null},
            };
        }

        [Test]
        public void Calling_GetCategory_with_CategoryId_input_should_return_Category_CategoryVM_from_database()
        {
            var input_categoryId = Literals.MiscMagicStrings.CategoryIdPrefix + "0000";
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetGategoryById(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(()=> new Category());

            mockSubCategoryRepository.Setup(x => x.GetSubCategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(()=>new SubCategory());

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            CategoryVM categoryVM = categoriesEngine.GetCategory(userId, input_categoryId);

            Assert.AreEqual(categoryVM.IsCategory, true);
        }

        [Test]
        public void Calling_GetCategory_with_SubCategoryId_input_should_return_SubCategory_CategoryVM_from_database()
        {
            
            var input_subcategoryId = Literals.MiscMagicStrings.SubcategoryIdPrefix + "0000";
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetGategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => new Category());

            mockSubCategoryRepository.Setup(x => x.GetSubCategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => new SubCategory());

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            CategoryVM categoryVM = categoriesEngine.GetCategory(userId, input_subcategoryId);

            Assert.AreEqual(categoryVM.IsCategory, false);
        }

        [Test]
        public void StoreNewCategoryAndSubCategory_should_call_repository_method()
        {
            mockCategoryRepository.Setup(x => x.StoreNewCategoryAndSubCategory(It.IsAny<Category>(),It.IsAny<SubCategory>()));

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.StoreNewCategoryAndSubCategory(new CategoryVM());

            mockCategoryRepository.VerifyAll();
        }

        [Test]
        public void StoreNewSubCategory_should_call_repository_method()
        {
            
            mockSubCategoryRepository.Setup(x => x.StoreSubCategory(It.IsAny<SubCategory>()));

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.StoreNewSubCategory(new CategoryVM());

            mockCategoryRepository.VerifyAll();
        }

        [Test]
        public void Calling_DeleteCategorySubCategories_with_CategoryId_input_should_remove_category_from_database()
        {
            var input_categoryId = Literals.MiscMagicStrings.CategoryIdPrefix + "0000";
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetGategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => new Category());
            mockCategoryRepository.Setup(x => x.Delete(It.IsAny<Category>()));

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.DeleteCategorySubCategories(userId, input_categoryId);

            mockCategoryRepository.VerifyAll();
        }

        [Test]
        public void Calling_DeleteCategorySubCategories_with_CategoryId_input_without_return_should_raise_an_error()
        {
            var input_categoryId = Literals.MiscMagicStrings.CategoryIdPrefix + "0000";
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetGategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => null);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            actionStatus = categoriesEngine.DeleteCategorySubCategories(userId, input_categoryId);

            
            Assert.AreEqual(actionStatus.ExceptionFromConditions, true);
            Assert.AreEqual(actionStatus.Status, false);
        }

        [Test]
        public void Calling_DeleteCategorySubCategories_with_SubCategoryId_input_should_remove_subcategory_from_database()
        {
           var input_categoryId = Literals.MiscMagicStrings.SubcategoryIdPrefix + "0000";
            var userId = "user1";

            mockSubCategoryRepository.Setup(x => x.GetSubCategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => new SubCategory());
            mockSubCategoryRepository.Setup(x => x.Delete(It.IsAny<SubCategory>()));

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.DeleteCategorySubCategories(userId, input_categoryId);

            mockSubCategoryRepository.VerifyAll();
        }

        [Test]
        public void Calling_DeleteCategorySubCategories_with_SubCategoryId_input_without_return_should_raise_an_error()
        {
            var input_categoryId = Literals.MiscMagicStrings.SubcategoryIdPrefix + "0000";
            var userId = "user1";

            mockSubCategoryRepository.Setup(x => x.GetSubCategoryById(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => null);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            actionStatus = categoriesEngine.DeleteCategorySubCategories(userId, input_categoryId);

            Assert.AreEqual(actionStatus.ExceptionFromConditions, true);
            Assert.AreEqual(actionStatus.Status, false);
        }

        [Test]
        public void EditCategoryAndSubCategory_if_categoryVM_IsCategory_should_update_database_through_categoryRepository()
        {
            
            mockCategoryRepository.Setup(x => x.UpdateCategory(It.IsAny<CategoryVM>()));

            var categoryVM = new CategoryVM() { IsCategory = true };

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.EditCategoryAndSubCategory(categoryVM);

            mockCategoryRepository.VerifyAll();
        }

        [Test]
        public void EditCategoryAndSubCategory_if_categoryVM_IsSubCategory_should_update_database_through_subcategoryRepository()
        {
            
            mockSubCategoryRepository.Setup(x => x.UpdateSubCategory(It.IsAny<CategoryVM>()));

            var categoryVM = new CategoryVM() { IsCategory = false };

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            categoriesEngine.EditCategoryAndSubCategory(categoryVM);

            mockSubCategoryRepository.VerifyAll();
        }

        [Test]
        public void GetAllGategoriesSubCategories_must_return_only_nonSpecial_categories()
        {
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetAllGategoriesSubCategories(It.IsAny<string>()))
                .Returns(()=> categoryListFromDatabase);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            var returnList = categoriesEngine.GetAllGategoriesSubCategories(userId);

            Assert.AreEqual(returnList.Count(), 3);

        }

        [Test]
        public void GetAllSpecialGategoriesSubCategories_must_return_only_Special_categories()
        {
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetAllGategoriesSubCategories(It.IsAny<string>()))
                .Returns(() => categoryListFromDatabase);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            var returnList = categoriesEngine.GetAllSpecialGategoriesSubCategories(userId);

            Assert.AreEqual(returnList.Count(), 2);

        }

        [Test]
        public void GetCategories_must_return_only_nonSpecial_categories()
        {
            var userId = "user1";

            mockCategoryRepository.Setup(x => x.GetGategories(It.IsAny<string>(), It.IsAny<TransactionType>()))
                .Returns(() => categoryListFromDatabase);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            var returnList = categoriesEngine.GetCategories(userId,TransactionType.Income);

            Assert.AreEqual(returnList.Count(), 3);
        }

        [Test]
        public void GetSubCategories_must_return_only_nonSpecial_categories()
        {
            var userId = "user1";
            var categoryId = 0;

            mockSubCategoryRepository.Setup(x => x.GetSubCategories(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => subCategoryListFromDatabase);

            var categoriesEngine = new CategoriesEngine(mockLogger.Object, actionStatus, mockCategoryRepository.Object, mockSubCategoryRepository.Object);

            var returnList = categoriesEngine.GetSubCategories(userId, categoryId);

            Assert.AreEqual(returnList.Count(), 3);
        }




    }
}
