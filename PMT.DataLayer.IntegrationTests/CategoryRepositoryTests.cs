using NUnit.Framework;
using PMT.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System.Data.Entity.Validation;
using PMT.Models;

namespace PMT.DataLayer.IntegrationTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private IUnityContainer container;
        private MainDb mainDb;
        private ICategoryRepository categoryRepository;
        private ISubCategoryRepository subCategoryRepository;
        private List<Category> categoryList;
        private List<SubCategory> subCategoryList;

        string userId = "userIdxxxxx";
        string otherUserId = "otherUserIdxxxxx";

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.GetConfiguredContainer();
            mainDb = container.Resolve<MainDb>();
            categoryRepository = container.Resolve<ICategoryRepository>();
            subCategoryRepository = container.Resolve<ISubCategoryRepository>();
            mainDb.Database.Initialize(true);

            categoryList = new List<Category>() {
                new Category(){CategoryId=1,Type=TransactionType.Income,UserId=userId,Name="Category1",Color="#123456",IconId="xx",SpecialAttribute=Literals.SpecialAttributes.AdjustmentExpense},
                new Category(){CategoryId=2,Type=TransactionType.Expense,UserId=userId,Name="Category2",Color="#123456",IconId="xx",SpecialAttribute=Literals.SpecialAttributes.AdjustmentExpense},
                new Category(){CategoryId=3,Type=TransactionType.Income,UserId=otherUserId,Name="Category3",Color="#123456",IconId="xx"},
                new Category(){CategoryId=4,Type=TransactionType.Income,UserId=userId,Name="Category4",Color="#123456",IconId="xx",SpecialAttribute=Literals.SpecialAttributes.AdjustmentExpense},
                new Category(){CategoryId=5,Type=TransactionType.Income,UserId=userId,Name="Category5",Color="#123456",IconId="xx"},
            };

            subCategoryList = new List<SubCategory>() {
                new SubCategory(){CategoryId=1,SubCategoryId=1,UserId=userId,Name="SubCategory1",Color="#123456",IconId="xx",SpecialAttribute=Literals.SpecialAttributes.AdjustmentExpense},
                new SubCategory(){CategoryId=1,SubCategoryId=2,UserId=userId,Name="SubCategory2",Color="#123456",IconId="xx"},
                new SubCategory(){CategoryId=3,SubCategoryId=3,UserId=otherUserId,Name="SubCategory3",Color="#123456",IconId="xx",SpecialAttribute=Literals.SpecialAttributes.AdjustmentExpense},
            };
        }

        [Test]
        public void GetGategories_should_return_categories_for_a_given_userid_and_transactionType()
        {

            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SaveChanges();
            mainDb.Categories.AddRange(categoryList);
            mainDb.SaveChanges();

            var sut = categoryRepository.GetGategories(userId, TransactionType.Income).ToList();


            Assert.That(sut.Count(), Is.EqualTo(3));
            Assert.AreEqual(sut[0].CategoryId, categoryList[0].CategoryId);
            Assert.AreEqual(sut[0].Type, categoryList[0].Type);
            Assert.AreEqual(sut[0].UserId, categoryList[0].UserId);
            Assert.AreEqual(sut[0].Name, categoryList[0].Name);
            Assert.AreEqual(sut[1].CategoryId, categoryList[3].CategoryId);
            Assert.AreEqual(sut[1].Type, categoryList[3].Type);
            Assert.AreEqual(sut[1].UserId, categoryList[3].UserId);
            Assert.AreEqual(sut[1].Name, categoryList[3].Name);
        }

        [Test]
        public void GetAllGategoriesSubCategories_should_return_all_categories_and_subcategories_for_a_userid()
        {

            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SubCategories.RemoveRange(mainDb.SubCategories.ToList());
            mainDb.SaveChanges();
            mainDb.Categories.AddRange(categoryList);
            mainDb.SubCategories.AddRange(subCategoryList);
            mainDb.SaveChanges();

            var sut = categoryRepository.GetAllGategoriesSubCategories(userId).ToList();

            Assert.AreEqual(sut.Count(), 4);
            Assert.AreEqual(sut.FirstOrDefault(x => x.Name == "Category1").SubCategories.Count(), 2);
            Assert.AreEqual(sut[0].CategoryId, categoryList[0].CategoryId);
            Assert.AreEqual(sut[0].Type, categoryList[0].Type);
            Assert.AreEqual(sut[0].UserId, categoryList[0].UserId);
            Assert.AreEqual(sut[0].Name, categoryList[0].Name);
            Assert.AreEqual(sut[3].CategoryId, categoryList[4].CategoryId);
            Assert.AreEqual(sut[3].Type, categoryList[4].Type);
            Assert.AreEqual(sut[3].UserId, categoryList[4].UserId);
            Assert.AreEqual(sut[3].Name, categoryList[4].Name);
            Assert.AreEqual(sut.FirstOrDefault(x => x.Name == "Category1").SubCategories.ToList()[0].SubCategoryId, subCategoryList[0].SubCategoryId);
            Assert.AreEqual(sut.FirstOrDefault(x => x.Name == "Category1").SubCategories.ToList()[1].SubCategoryId, subCategoryList[1].SubCategoryId);

        }

        [Test]
        public void GetGategoryById_should_return_categories_for_a_given_userid_and_categoryId()
        {
            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SaveChanges();
            mainDb.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Categories', RESEED, 0)");
            mainDb.Categories.AddRange(categoryList);
            mainDb.SaveChanges();

            var sut = categoryRepository.GetGategoryById(userId, 4);

            Assert.AreEqual(sut.CategoryId, 4);
            Assert.AreEqual(sut.Name, "Category4");

        }

        [Test]
        public void GetGategoryByName_should_return_a_category_for_a_user_by_a_given_categoryname_and_userid()
        {
            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SaveChanges();
            mainDb.Categories.AddRange(categoryList);
            mainDb.SaveChanges();

            var sut = categoryRepository.GetGategoryByName(userId, "Category4");

            Assert.AreEqual(categoryList.FirstOrDefault(x => x.Name == "Category4" && x.UserId == userId).CategoryId, sut.CategoryId);
        }

        [Test]
        public void StoreNewCategoryAndSubCategory_should_save_in_database_category_and_subcategory()
        {
            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SubCategories.RemoveRange(mainDb.SubCategories.ToList());
            mainDb.SaveChanges();


            var category = categoryList.FirstOrDefault(x=>x.Name== "Category1");
            var subCategory = subCategoryList.FirstOrDefault(x => x.Name == "SubCategory1");

            categoryRepository.StoreNewCategoryAndSubCategory(category, subCategory);
            var retrieved_category = mainDb.Categories.FirstOrDefault(x=>x.Name==category.Name);
            var retrieved_subCategory = mainDb.SubCategories.FirstOrDefault(x => x.Name == subCategory.Name);

            Assert.AreEqual(category.Name, retrieved_category.Name);
            Assert.AreEqual(category.Type, retrieved_category.Type);
            Assert.AreEqual(category.UserId, retrieved_category.UserId);
            Assert.AreEqual(category.Color, retrieved_category.Color);
            Assert.AreEqual(category.IconId, retrieved_category.IconId);
            Assert.AreEqual(category.SpecialAttribute, retrieved_category.SpecialAttribute);

            Assert.AreEqual(subCategory.Name, retrieved_subCategory.Name);
            Assert.AreEqual(subCategory.CategoryId, retrieved_subCategory.CategoryId);
            Assert.AreEqual(subCategory.UserId, retrieved_subCategory.UserId);
            Assert.AreEqual(subCategory.Color, retrieved_subCategory.Color);
            Assert.AreEqual(subCategory.IconId, retrieved_subCategory.IconId);
            Assert.AreEqual(subCategory.SpecialAttribute, retrieved_subCategory.SpecialAttribute);


        }

        [Test]
        public void StoreNewCategoryAndSubCategory_should_not_save_in_database_empty_category_subcategory()
        {
            var category = new Category();
            var subCategory = new SubCategory();

            Assert.Throws<DbEntityValidationException>(()=> categoryRepository.StoreNewCategoryAndSubCategory(category, subCategory));

        }

        [Test]
        public void UpdateCategory_should_update_category_in_database()
        {
            var category = categoryList.FirstOrDefault(x=>x.Name=="Category2");

            mainDb.Categories.RemoveRange(mainDb.Categories.ToList());
            mainDb.SaveChanges();

            mainDb.Categories.Add(category);
            mainDb.SaveChanges();

            var retrievedCategory = mainDb.Categories.FirstOrDefault(x => x.Name == "Category2");

            retrievedCategory.Name = "UpdatedCategory2";
            retrievedCategory.Color = "#112233";
            retrievedCategory.SpecialAttribute = Literals.SpecialAttributes.TransferIncome;

            var categoryVMToUpdate = new Mapping().CategoryToCategoryVM(retrievedCategory);
            mainDb.SaveChanges();
            categoryRepository.UpdateCategory(categoryVMToUpdate);

            var updatedCategory = mainDb.Categories.FirstOrDefault(x => x.Name == "UpdatedCategory2");

            Assert.That(updatedCategory, Is.Not.Null);
            Assert.That(updatedCategory.Color, Is.EqualTo(retrievedCategory.Color));
            Assert.That(updatedCategory.SpecialAttribute, Is.EqualTo(retrievedCategory.SpecialAttribute));
        }

        [Test]
        public void UpdateCategory_should_not_update_with_empty_categoryVM()
        {
            var categoryVM = new CategoryVM();

            Assert.Catch<Exception>(() => categoryRepository.UpdateCategory(categoryVM));

        }

        [Test]
        public void Delete_should_remove_category_from_database()
        {
            mainDb.Categories.RemoveRange(mainDb.Categories);
            mainDb.SaveChanges();

            var category = categoryList.FirstOrDefault(x => x.Name == "Category2");
            mainDb.Categories.Add(category);
            mainDb.SaveChanges();

            var categoryInDatabase = categoryRepository.GetGategoryByName(userId, "Category2");

            categoryRepository.Delete(categoryInDatabase);

            var retrievedCategory = mainDb.Categories.FirstOrDefault(x => x.Name == "Category2");

            Assert.That(categoryInDatabase, Is.Not.Null);
            Assert.That(retrievedCategory, Is.Null);

        }

        [Test]
        public void Delete_should_raise_exception_with_empty_category()
        {
            var category = new Category();

            Assert.Catch<Exception>(() => categoryRepository.Delete(category));
        }
    }
}
