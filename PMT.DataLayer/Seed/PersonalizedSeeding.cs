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

        public PersonalizedSeeding(ISeedingLists seedingLists)
        {
            this.seedingLists = seedingLists;
        }

        public MoneyAccount GetDefaultAccountForNewUser(PMT.DataLayer.MainDb context,string userId)
        {
            return new MoneyAccount { UserId = userId, MoneyAccountId = 0, Name =  ViewText.Personal};
        }

        public void Categories(PMT.DataLayer.MainDb context,string userId)
        {
            List<Category> MainCategoryList = seedingLists.GetMainCategories();
            List<SubCategory> SubCategoryList = seedingLists.GetSubCategries();

            foreach (var item in MainCategoryList)
                context.Categories.AddOrUpdate(
                    p => p.CategoryId,
                    new Category
                    {
                        Type = item.Type,
                        CategoryId = item.CategoryId,
                        Name = item.Name,
                        IconId = item.IconId,
                        Color = item.Color,
                        UserId = userId
                    });

            foreach (var item in SubCategoryList)
                context.SubCategories.AddOrUpdate(
                    p => p.SubCategoryId,
                    new SubCategory
                    {
                        CategoryId = item.CategoryId,
                        SubCategoryId = item.SubCategoryId,
                        Name = item.Name,
                        IconId = item.IconId,
                        Color = item.Color,
                        UserId = userId
                    });

            context.SaveChanges();
        }
    }
}
