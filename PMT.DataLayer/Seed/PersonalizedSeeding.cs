﻿using PMT.Common.Resources;
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
