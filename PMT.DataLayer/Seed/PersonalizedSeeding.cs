using PMT.Common;
using PMT.Common.Resources;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Seed
{
    public class PersonalizedSeeding : IPersonalizedSeeding
    {
        ISeedingLists seedingLists;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        ICurrentDateTime currentDateTime;
        MainDb context;
        public PersonalizedSeeding(MainDb context,ISeedingLists seedingLists, ICurrentDateTime currentDateTime,
        ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
        {
            this.context = context;
            this.seedingLists = seedingLists;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.currentDateTime = currentDateTime;
        }

        public List<Transaction> GetDemoData(string userId,int moneyAccountId)
        {
            var list = new List<Transaction>();
            list.Add(new Transaction() {
                UserId = userId,
                Amount=2000,
                MoneyAccountId= moneyAccountId,
                TransactionDate= currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(1),
                Description="My Salary",
                CategoryId=categoryRepository.GetGategoryByName(userId,SeedingDataText.Salary).CategoryId,
                SubCategoryId=subCategoryRepository.GetSubCategoryByName(userId,SeedingDataText.Salary).SubCategoryId,
                TransactionType=TransactionType.Income
            });
            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 400,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(15),
                Description = "My Bonus",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Bonus).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.Bonus).SubCategoryId,
                TransactionType = TransactionType.Income
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 640,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(4),
                Description = "Home sweet home",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Home).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.Rent).SubCategoryId,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 65,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(7),
                Description = "Everything for Garfield",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Pets).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.Pets).SubCategoryId,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 80,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(11),
                Description = "",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Transport).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.PublicTransport).SubCategoryId,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 442,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(7),
                Description = "Food for vegans and stuff",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Food).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.Groceries).SubCategoryId,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 270,
                MoneyAccountId = moneyAccountId,
                TransactionDate = currentDateTime.DateTimeUtcNow().OffsetInCurrentMonth(16),
                Description = "",
                CategoryId = categoryRepository.GetGategoryByName(userId, SeedingDataText.Utilities).CategoryId,
                SubCategoryId = subCategoryRepository.GetSubCategoryByName(userId, SeedingDataText.Electricity).SubCategoryId,
                TransactionType = TransactionType.Expense
            });

            return list;
        }

        public MoneyAccount GetDefaultAccountForNewUser(string userId)
        {
            return new MoneyAccount { UserId = userId, MoneyAccountId = 0, Name =  ViewText.Personal};
        }

        public void Categories(string userId)
        {
            List<Category> MainCategoryList = seedingLists.GetMainCategories();
            List<SubCategory> SubCategoryList = seedingLists.GetSubCategries();

            foreach (var item in MainCategoryList)
                context.Categories.AddOrUpdate(
                    p => new { p.CategoryId , p.UserId},
                    new Category
                    {
                        Type = item.Type,
                        CategoryId = item.CategoryId,
                        Name = item.Name,
                        IconId = item.IconId,
                        Color = item.Color,
                        UserId = userId,
                        SpecialAttribute= item.SpecialAttribute
                    });

            foreach (var item in SubCategoryList)
                context.SubCategories.AddOrUpdate(
                    p => new { p.SubCategoryId,p.UserId },
                    new SubCategory
                    {
                        CategoryId = item.CategoryId,
                        SubCategoryId = item.SubCategoryId,
                        Name = item.Name,
                        IconId = item.IconId,
                        Color = item.Color,
                        UserId = userId,
                        SpecialAttribute = item.SpecialAttribute
                    });

            context.SaveChanges();
        }
    }
}
