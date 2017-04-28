namespace PMT.DataLayer.Context.MainMigrations
{
    using PMT.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PMT.DataLayer.Seeding;

    internal sealed class Configuration : DbMigrationsConfiguration<PMT.DataLayer.MainDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Context\MainMigrations";
        }

        protected override void Seed(PMT.DataLayer.MainDb context)
        {
            var Seeds = new Seeding();

            List<Category> MainCategoryList = Seeds.GetMainCategories();
            List<SubCategory> SubCategoryList = Seeds.GetSubCategries();
            List<Repeat> RepeatList = Seeds.GetRepeats();

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
                        RepeatId=item.RepeatId,
                        Description=item.Description,
                        AddDays=item.AddDays,
                        AddWeeks=item.AddWeeks,
                        AddMonths=item.AddMonths,
                        AddYears=item.AddYears
                    });

        }
    }
}
