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
        List<Icon> IconList = new List<Icon>();


        public SeedingLists()
        {
            
            
        }


        public MoneyAccount GetDefaultAccountForNewUser(string userId)
        {
            return new MoneyAccount { UserId=userId,MoneyAccountId=0,Name="Personal"};
        }

        public List<Icon> GetIcons()
        {
            PrepareIcons();
            return IconList.DistinctBy(x => x.IconId).ToList();
        }

        public void PrepareIcons()
        {
            var seedIcons = new SeedIcons();
            seedIcons.AddFontFromCssToList(IconList, "~/Content/font-awesome.css", "fa-", "fontawesome");
            seedIcons.AddFontFromCssToList(IconList, "~/css/whhg.css", "icon-", "whhg");
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

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "fa-money", Type = TransactionType.Income, Name = "Salary" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "fa-money", CategoryId = i, Name = "Salary" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-value-coins", Type = TransactionType.Income, Name = "Bonus" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-value-coins", CategoryId = i, Name = "Bonus" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-creditcard", Type = TransactionType.Income, Name = "Borrow" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-creditcard", CategoryId = i, Name = "Borrow" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-percent", Type = TransactionType.Income, Name = "Interest" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-percent", CategoryId = i, Name = "Interest" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-money-cash", Type = TransactionType.Income, Name = "Collectin debts" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-money-cash", CategoryId = i, Name = "Collectin debts" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-gift", Type = TransactionType.Income, Name = "Gift" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-gift", CategoryId = i, Name = "Gift" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-dollar", Type = TransactionType.Income, Name = "Other" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-dollar", CategoryId = i, Name = "Other" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-stickynote", Type = TransactionType.Expense, Name = "Misc" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-stickynote", CategoryId = i, Name = "Misc" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-stickynotealt", Type = TransactionType.Expense, Name = "General" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-stickynotealt", CategoryId = i, Name = "General" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-hanger", Type = TransactionType.Expense, Name = "Clothing" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-hanger", CategoryId = i, Name = "Clothing" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-stiletto", Type = TransactionType.Expense, Name = "Shoes" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-stiletto", CategoryId = i, Name = "Shoes" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-student-school", Type = TransactionType.Expense, Name = "Education" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-student-school", CategoryId = i, Name = "Education" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bookalt", CategoryId = i, Name = "Books" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-podium", CategoryId = i, Name = "Courses" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bookthree", CategoryId = i, Name = "Tuition" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-fork", Type = TransactionType.Expense, Name = "Food" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-fork", CategoryId = i, Name = "Food" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-store", CategoryId = i, Name = "Restaurant" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-shopping-cart", CategoryId = i, Name = "Groceries" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-gift", Type = TransactionType.Expense, Name = "Gifts" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-gift", CategoryId = i, Name = "Gifts" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-firstaid", Type = TransactionType.Expense, Name = "Health" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-firstaid", CategoryId = i, Name = "Health" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-stethoscope", CategoryId = i, Name = "Doctor" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-toothbrush", CategoryId = i, Name = "Dentist" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-hospital-o", CategoryId = i, Name = "Clinic" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-shield", CategoryId = i, Name = "Medical Insurance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bottle", CategoryId = i, Name = "Medicines" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-medkit", CategoryId = i, Name = "Therapy" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-glasses", CategoryId = i, Name = "Glasses" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-home", Type = TransactionType.Expense, Name = "Home" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-home", CategoryId = i, Name = "Home" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-broom", CategoryId = i, Name = "Home services" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bank", CategoryId = i, Name = "Mortage" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-bed", CategoryId = i, Name = "Rent" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-fence", CategoryId = i, Name = "Home Maintance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-plug", CategoryId = i, Name = "Appliances" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bed", CategoryId = i, Name = "Furniture" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-umbrella", CategoryId = i, Name = "Home Insurance" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-baby", Type = TransactionType.Expense, Name = "Kids" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-baby", CategoryId = i, Name = "Kids" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-coinsalt", CategoryId = i, Name = "Allowance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-milk", CategoryId = i, Name = "Baby Supplies" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-stroller", CategoryId = i, Name = "Babysitter" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-pacifier", CategoryId = i, Name = "Daycare" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-paperplane", CategoryId = i, Name = "Toys" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-abacus", Type = TransactionType.Expense, Name = "Taxes" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-abacus", CategoryId = i, Name = "Taxes" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-creditcard", Type = TransactionType.Expense, Name = "Lend" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-creditcard", CategoryId = i, Name = "Lend" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-mastercard", Type = TransactionType.Expense, Name = "Repaiment" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-mastercard", CategoryId = i, Name = "Repaiment" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-manalt", Type = TransactionType.Expense, Name = "Personal" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-manalt", CategoryId = i, Name = "Personal" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-mirror", CategoryId = i, Name = "Grooming" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-weightscale", CategoryId = i, Name = "Gym" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-lipstick", CategoryId = i, Name = "Cosmetics" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-diamond", CategoryId = i, Name = "Jewelry" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-paperplane", Type = TransactionType.Expense, Name = "Hobbies" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-paperplane", CategoryId = i, Name = "Hobbies" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-bone", Type = TransactionType.Expense, Name = "Pets" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-bone", CategoryId = i, Name = "Pets" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-boat", Type = TransactionType.Expense, Name = "Recreation" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-boat", CategoryId = i, Name = "Recreation" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-coffee", CategoryId = i, Name = "Coffe/Bar" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-butterflyalt", CategoryId = i, Name = "Parks" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-facetime-video", CategoryId = i, Name = "Movies" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-headphonesthree", CategoryId = i, Name = "Music" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-suitcase", CategoryId = i, Name = "Vacation" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-die-dice", CategoryId = i, Name = "Gambling" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-tennis", Type = TransactionType.Expense, Name = "Sport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-tennis", CategoryId = i, Name = "Sport" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "icon-automobile-car", Type = TransactionType.Expense, Name = "Transport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "icon-automobile-car", CategoryId = i, Name = "Transport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-gasstation", CategoryId = i, Name = "Fuel" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-life-ring", CategoryId = i, Name = "Car Insurance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-screwdriver", CategoryId = i, Name = "Car Maintenance" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-freeway", CategoryId = i, Name = "Tolls" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-parkeddomain", CategoryId = i, Name = "Parking" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-bus", CategoryId = i, Name = "Public Transport" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-taxi", CategoryId = i, Name = "Taxi" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-plane", CategoryId = i, Name = "Travel" });

            MainCategoryList.Add(new Category { CategoryId = i++, Color= "", IconId = "fa-wrench", Type = TransactionType.Expense, Name = "Utilities" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color = "", IconId = "fa-wrench", CategoryId = i, Name = "Utilities" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-voltage", CategoryId = i, Name = "Electricity" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-construction", CategoryId = i, Name = "City" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "fa-fire", CategoryId = i, Name = "Gas" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-phonealt", CategoryId = i, Name = "Home Phone" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-iphone", CategoryId = i, Name = "Mobile Phone" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-circlefacebook", CategoryId = i, Name = "Internet" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-tv", CategoryId = i, Name = "Television" });
            SubCategoryList.Add(new SubCategory { SubCategoryId = y++, Color= "", IconId = "icon-watertap-plumbing", CategoryId = i, Name = "Water" });

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
