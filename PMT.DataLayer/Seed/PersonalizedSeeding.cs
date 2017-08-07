using PMT.Common;
using PMT.Common.Resources;
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
        MainDb context;
        public PersonalizedSeeding(MainDb context,ISeedingLists seedingLists)
        {
            this.context = context;
            this.seedingLists = seedingLists;
        }

        public List<Transaction> GetDemoData(string userId)
        {
            var list = new List<Transaction>();
            list.Add(new Transaction() {
                UserId = userId,
                Amount=4400,
                MoneyAccountId=1,
                TransactionDate=DateTime.UtcNow.OffsetInCurrentMonth(1),
                Description="My Salary",
                CategoryId=5,
                SubCategoryId=5,
                TransactionType=TransactionType.Income
            });
            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 1200,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(15),
                Description = "My Bonus",
                CategoryId = 6,
                SubCategoryId = 6,
                TransactionType = TransactionType.Income
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 640,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(4),
                Description = "Home sweet home",
                CategoryId = 19,
                SubCategoryId = 35,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 65,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(7),
                Description = "Everything for Garfield",
                CategoryId = 26,
                SubCategoryId = 55,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 80,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(11),
                Description = "",
                CategoryId = 29,
                SubCategoryId = 70,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 442,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(7),
                Description = "Food for vegans and stuff",
                CategoryId = 16,
                SubCategoryId = 22,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 900,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(16),
                Description = "mi casa",
                CategoryId = 19,
                SubCategoryId = 34,
                TransactionType = TransactionType.Expense
            });

            list.Add(new Transaction()
            {
                UserId = userId,
                Amount = 270,
                MoneyAccountId = 1,
                TransactionDate = DateTime.UtcNow.OffsetInCurrentMonth(16),
                Description = "",
                CategoryId = 30,
                SubCategoryId = 73,
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
