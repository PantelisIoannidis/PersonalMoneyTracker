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
    public class CommonSeeding
    {
        public CommonSeeding()
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
            List<Icon> IconList = seedingLists.GetIcons();


            if (IconList.Count() > 0)
            {
                var entries = context.Icons;
                context.Icons.RemoveRange(entries);
                context.Icons.AddRange(IconList);
                context.SaveChanges();
            }



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
