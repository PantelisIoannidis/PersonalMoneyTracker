using PMT.Common;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Seed
{
    public class SeedingLists : ISeedingLists
    {
        List<Category> MainCategoryList = new List<Category>();
        List<SubCategory> SubCategoryList = new List<SubCategory>();
        List<Repeat> RepeatList = new List<Repeat>();


        public SeedingLists()
        {
            
            
        }


        public MoneyAccount GetDefaultAccountForNewUser(string userId)
        {
            return new MoneyAccount { UserId=userId,MoneyAccountId=0,Name="Personal"};
        }

        public List<Category> GetMainCategories()
        {
            PrepareCategories();
            return MainCategoryList;
        }

        public List<SubCategory> GetSubCategries()
        {
            PrepareCategories();
            return SubCategoryList;
        }

        public List<Repeat> GetRepeats()
        {
            PrepareRepeats();
            return RepeatList;
        }

        private void PrepareCategories()
        {
            int i = 0;
            int y = 0;

            if (MainCategoryList.Count > 0) return;

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Salary" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Bonus" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Borrow" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Interest" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Collectin debts" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Gift" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Income, Name = "Other" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "None" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "General" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Clothing" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Education" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Books" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Courses" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Tuition" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Food" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Restaurant" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Groceries" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Gifts" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Health" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Doctor" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Dentist" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Clinic" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Medical Insurance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Medicines" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Therapy" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Glasses" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Home" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Home services" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Mortage" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Rent" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Home Maintance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Appliances" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Furniture" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Home Insurance" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Kids" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Allowance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Baby Supplies" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Babysitter" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Daycare" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Toys" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Taxes" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Lend" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Repaiment" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Personal" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Hobbies" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Pets" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Recreation" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Movies" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Music" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Holidays" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Gambling" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Sport" });
            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Transport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Fuel" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Car Insurance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Car Maintenance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Parking" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Public Transport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Taxi" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Travel" });

            MainCategoryList.Add(new Category { CategoryId = i++, Type = TransactionType.Expense, Name = "Utilities" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Electricity" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "City" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Gas" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Home Phone" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Mobile Phone" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Internet" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Television" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, CategoryId = i, Name = "Water" });

        }

        private void PrepareRepeats()
        {
            int i = 0;

            if (RepeatList.Count > 0) return;

            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Daily", AddDays = 1, AddWeeks = 0, AddMonths = 0, AddYears = 0 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Weekly", AddDays = 0, AddWeeks = 1, AddMonths = 0, AddYears = 0 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Monthly", AddDays = 0, AddWeeks = 0, AddMonths = 1, AddYears = 0 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Yearly", AddDays = 0, AddWeeks = 0, AddMonths = 0, AddYears = 1 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Two Weekly", AddDays = 0, AddWeeks = 2, AddMonths = 0, AddYears = 0 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Four Monthly", AddDays = 0, AddWeeks = 0, AddMonths = 4, AddYears = 0 });
            RepeatList.Add(new Repeat { RepeatId = i++, Description = "Quarterly", AddDays = 0, AddWeeks = 0, AddMonths = 3, AddYears = 0 });

        }

    }
}
