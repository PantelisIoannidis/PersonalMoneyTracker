using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace PMT.DataLayer.Seed
{
    public class Seeding
    {
        public Seeding()
        {

        }

        public void Seed(PMT.DataLayer.IdentityDb context)
        {

        }

        public void Seed(PMT.DataLayer.MainDb context)
        {
            var seedingLists = new SeedingLists();

            List<Category> MainCategoryList = seedingLists.GetMainCategories();
            List<SubCategory> SubCategoryList = seedingLists.GetSubCategries();
            List<Repeat> RepeatList = seedingLists.GetRepeats();

            foreach (var item in MainCategoryList)
                context.Categories.AddOrUpdate(
                    p => p.CategoryId,
                    new Category
                    {
                        Type = item.Type,
                        CategoryId = item.CategoryId,
                        Name = item.Name
                    });

            foreach (var item in SubCategoryList)
                context.SubCategories.AddOrUpdate(
                    p => p.SubCategoryId,
                    new SubCategory
                    {
                        CategoryId = item.CategoryId,
                        SubCategoryId = item.SubCategoryId,
                        Name = item.Name
                    });

            foreach (var item in RepeatList)
                context.Repeats.AddOrUpdate(
                    p => p.RepeatId,
                    new Repeat
                    {
                        RepeatId = item.RepeatId,
                        Description = item.Description,
                        AddDays = item.AddDays,
                        AddWeeks = item.AddWeeks,
                        AddMonths = item.AddMonths,
                        AddYears = item.AddYears
                    });

        }
    }
}
