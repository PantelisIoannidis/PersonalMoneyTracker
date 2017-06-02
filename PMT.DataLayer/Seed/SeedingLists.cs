using PMT.Common;
using PMT.Common.Resources;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PMT.Entities.Literals;

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
            if (MainCategoryList.Count > 0) return;

            MainCategoryList.Add(new Category { CategoryId = StandarCategories.TransferFrom, Color = "#666666", IconId = "fa-exchange", Type = TransactionType.Income, Name = SeedingDataText.TransferFrom });
            SubCategoryList.Add(new SubCategory { SubCategoryId = StandarCategories.TransferFrom, Color = "#666666", IconId = "fa-exchange", CategoryId = StandarCategories.TransferFrom, Name = SeedingDataText.TransferFrom });
            MainCategoryList.Add(new Category { CategoryId = StandarCategories.TransferTo, Color = "#666666", IconId = "fa-exchange", Type = TransactionType.Expense, Name = SeedingDataText.TransferTo });
            SubCategoryList.Add(new SubCategory { SubCategoryId = StandarCategories.TransferTo, Color = "#666666", IconId = "fa-exchange", CategoryId = StandarCategories.TransferTo, Name = SeedingDataText.TransferTo });
            MainCategoryList.Add(new Category { CategoryId = StandarCategories.AdjustmentIncome, Color = "#999966", IconId = "fa-balance-scale", Type = TransactionType.Expense, Name = SeedingDataText.Adjustment });
            SubCategoryList.Add(new SubCategory { SubCategoryId = StandarCategories.AdjustmentIncome, Color = "#999966", IconId = "fa-balance-scale", CategoryId = StandarCategories.AdjustmentIncome, Name = SeedingDataText.Adjustment });
            MainCategoryList.Add(new Category { CategoryId = StandarCategories.AdjustmentExpense, Color = "#999966", IconId = "fa-balance-scale", Type = TransactionType.Expense, Name = SeedingDataText.Adjustment });
            SubCategoryList.Add(new SubCategory { SubCategoryId = StandarCategories.AdjustmentExpense, Color = "#999966", IconId = "fa-balance-scale", CategoryId = StandarCategories.AdjustmentExpense, Name = SeedingDataText.Adjustment });

            int i = StandarCategories.Count;
            int y = StandarCategories.Count;


            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#e6b800", IconId = "fa-money", Type = TransactionType.Income, Name = SeedingDataText.Salary });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#e6b800", IconId = "fa-money", CategoryId = i, Name = SeedingDataText.Salary });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#86b300", IconId = "icon-value-coins", Type = TransactionType.Income, Name = SeedingDataText.Bonus });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#86b300", IconId = "icon-value-coins", CategoryId = i, Name = SeedingDataText.Bonus });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#cc2900", IconId = "icon-creditcard", Type = TransactionType.Income, Name = SeedingDataText.Borrow });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc2900", IconId = "icon-creditcard", CategoryId = i, Name = SeedingDataText.Borrow });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#801a00", IconId = "icon-percent", Type = TransactionType.Income, Name = SeedingDataText.Interest });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#801a00", IconId = "icon-percent", CategoryId = i, Name = SeedingDataText.Interest });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#b30086", IconId = "icon-money-cash", Type = TransactionType.Income, Name = SeedingDataText.Collectindebts });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b30086", IconId = "icon-money-cash", CategoryId = i, Name = SeedingDataText.Collectindebts });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#bb99ff", IconId = "icon-gift", Type = TransactionType.Income, Name = SeedingDataText.Gift });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#bb99ff", IconId = "icon-gift", CategoryId = i, Name = SeedingDataText.Gift });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#00cc88", IconId = "icon-dollar", Type = TransactionType.Income, Name = SeedingDataText.Misc });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00cc88", IconId = "icon-dollar", CategoryId = i, Name = SeedingDataText.Misc });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#00cc88", IconId = "icon-stickynote", Type = TransactionType.Expense, Name = SeedingDataText.Misc });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00cc88", IconId = "icon-stickynote", CategoryId = i, Name = SeedingDataText.Misc });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#006644", IconId = "icon-stickynotealt", Type = TransactionType.Expense, Name = SeedingDataText.General });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#006644", IconId = "icon-stickynotealt", CategoryId = i, Name = SeedingDataText.General });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#cc5200", IconId = "icon-hanger", Type = TransactionType.Expense, Name = SeedingDataText.Clothing });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc5200", IconId = "icon-hanger", CategoryId = i, Name = SeedingDataText.Clothing });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ff4da6", IconId = "icon-stiletto", CategoryId = i, Name = SeedingDataText.Shoes });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#888844", IconId = "icon-student-school", Type = TransactionType.Expense, Name = SeedingDataText.Education });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#888844", IconId = "icon-student-school", CategoryId = i, Name = SeedingDataText.Education });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b3b300", IconId = "icon-bookalt", CategoryId = i, Name = SeedingDataText.Books });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ff5500", IconId = "icon-podium", CategoryId = i, Name = SeedingDataText.Courses });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc0088", IconId = "icon-bookthree", CategoryId = i, Name = SeedingDataText.Tuition });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#ffcc00", IconId = "icon-fork", Type = TransactionType.Expense, Name = SeedingDataText.Food });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ffcc00", IconId = "icon-fork", CategoryId = i, Name = SeedingDataText.Food });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#8c1aff", IconId = "icon-store", CategoryId = i, Name = SeedingDataText.Restaurant });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00cc88", IconId = "fa-shopping-cart", CategoryId = i, Name = SeedingDataText.Groceries });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#e600e6", IconId = "icon-gift", Type = TransactionType.Expense, Name = SeedingDataText.Gift });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#e600e6", IconId = "icon-gift", CategoryId = i, Name = SeedingDataText.Gifts });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#666600", IconId = "icon-firstaid", Type = TransactionType.Expense, Name = SeedingDataText.Health });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#666600", IconId = "icon-firstaid", CategoryId = i, Name = SeedingDataText.Health });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cccc00", IconId = "fa-stethoscope", CategoryId = i, Name = SeedingDataText.Doctor });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#40ff00", IconId = "icon-toothbrush", CategoryId = i, Name = SeedingDataText.Dentist });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00b38f", IconId = "fa-hospital-o", CategoryId = i, Name = SeedingDataText.Clinic });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#730099", IconId = "fa-shield", CategoryId = i, Name = SeedingDataText.MedicalInsurance });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#008040", IconId = "icon-bottle", CategoryId = i, Name = SeedingDataText.Medicines });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#003399", IconId = "fa-medkit", CategoryId = i, Name = SeedingDataText.Therapy });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ff944d", IconId = "icon-glasses", CategoryId = i, Name = SeedingDataText.Glasses });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#cc0000", IconId = "icon-home", Type = TransactionType.Expense, Name = SeedingDataText.Home });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc0000", IconId = "icon-home", CategoryId = i, Name = SeedingDataText.Home });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ffb31a", IconId = "icon-broom", CategoryId = i, Name = SeedingDataText.HomeServices });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#86b300", IconId = "icon-bank", CategoryId = i, Name = SeedingDataText.Mortage });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#004d3d", IconId = "fa-bed", CategoryId = i, Name = SeedingDataText.Rent });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ac00e6", IconId = "icon-fence", CategoryId = i, Name = SeedingDataText.HomeMaintance });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00b359", IconId = "fa-plug", CategoryId = i, Name = SeedingDataText.Appliances });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#004de6", IconId = "icon-bed", CategoryId = i, Name = SeedingDataText.Furniture });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#1affff", IconId = "icon-umbrella", CategoryId = i, Name = SeedingDataText.HomeInsurance });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#b30059", IconId = "icon-baby", Type = TransactionType.Expense, Name = SeedingDataText.Kids });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b30059", IconId = "icon-baby", CategoryId = i, Name = SeedingDataText.Kids });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b37700", IconId = "icon-coinsalt", CategoryId = i, Name = SeedingDataText.Allowance });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#7a7a52", IconId = "icon-milk", CategoryId = i, Name = SeedingDataText.BabySupplies });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00ff55", IconId = "icon-stroller", CategoryId = i, Name = SeedingDataText.Babysitter });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ffff00", IconId = "icon-pacifier", CategoryId = i, Name = SeedingDataText.Daycare });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#4d0099", IconId = "icon-paperplane", CategoryId = i, Name = SeedingDataText.Toys });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#9900ff", IconId = "icon-abacus", Type = TransactionType.Expense, Name = SeedingDataText.Taxes });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#9900ff", IconId = "icon-abacus", CategoryId = i, Name = SeedingDataText.Taxes });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#0039e6", IconId = "icon-creditcard", Type = TransactionType.Expense, Name = SeedingDataText.Lend });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#0039e6", IconId = "icon-creditcard", CategoryId = i, Name = SeedingDataText.Lend });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#99e600", IconId = "icon-mastercard", Type = TransactionType.Expense, Name = SeedingDataText.Repaiment });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#99e600", IconId = "icon-mastercard", CategoryId = i, Name = SeedingDataText.Repaiment });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#99994d", IconId = "icon-manalt", Type = TransactionType.Expense, Name = SeedingDataText.Personal });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#99994d", IconId = "icon-manalt", CategoryId = i, Name = SeedingDataText.Personal });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#4d4d33", IconId = "icon-mirror", CategoryId = i, Name = SeedingDataText.Grooming });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#269900", IconId = "icon-weightscale", CategoryId = i, Name = SeedingDataText.Gym });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#6600cc", IconId = "icon-lipstick", CategoryId = i, Name = SeedingDataText.Cosmetics });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b30086", IconId = "fa-diamond", CategoryId = i, Name = SeedingDataText.Jewelry });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#bf8040", IconId = "icon-paperplane", Type = TransactionType.Expense, Name = SeedingDataText.Hobbies });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#bf8040", IconId = "icon-paperplane", CategoryId = i, Name = SeedingDataText.Hobbies });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#39ac73", IconId = "icon-bone", Type = TransactionType.Expense, Name = SeedingDataText.Pets });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#39ac73", IconId = "icon-bone", CategoryId = i, Name = SeedingDataText.Pets });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#e65c00", IconId = "icon-boat", Type = TransactionType.Expense, Name = SeedingDataText.Recreation });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#e65c00", IconId = "icon-boat", CategoryId = i, Name = SeedingDataText.Recreation });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#e6e600", IconId = "fa-coffee", CategoryId = i, Name = SeedingDataText.CoffeBar });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#999900", IconId = "icon-butterflyalt", CategoryId = i, Name = SeedingDataText.Parks });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#4d4d00", IconId = "icon-facetime-video", CategoryId = i, Name = SeedingDataText.Movies });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00997a", IconId = "icon-headphonesthree", CategoryId = i, Name = SeedingDataText.Music });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#0040ff", IconId = "fa-suitcase", CategoryId = i, Name = SeedingDataText.Vacation });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#006680", IconId = "icon-die-dice", CategoryId = i, Name = SeedingDataText.Gambling });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#264d00", IconId = "icon-tennis", Type = TransactionType.Expense, Name = SeedingDataText.Sport });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#264d00", IconId = "icon-tennis", CategoryId = i, Name = SeedingDataText.Sport });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#009900", IconId = "icon-automobile-car", Type = TransactionType.Expense, Name = SeedingDataText.Transport });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#009900", IconId = "icon-automobile-car", CategoryId = i, Name = SeedingDataText.Transport });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#e6e600", IconId = "icon-gasstation", CategoryId = i, Name = SeedingDataText.Fuel });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#00b3b3", IconId = "fa-life-ring", CategoryId = i, Name = SeedingDataText.CarInsurance });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#0040ff", IconId = "icon-screwdriver", CategoryId = i, Name = SeedingDataText.CarMaintenance });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#a300cc", IconId = "icon-freeway", CategoryId = i, Name = SeedingDataText.Tolls });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc0099", IconId = "icon-parkeddomain", CategoryId = i, Name = SeedingDataText.Parking });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#006666", IconId = "icon-bus", CategoryId = i, Name = SeedingDataText.PublicTransport });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#c3c388", IconId = "icon-taxi", CategoryId = i, Name = SeedingDataText.Taxi });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ff3333", IconId = "icon-plane", CategoryId = i, Name = SeedingDataText.Travel });

            MainCategoryList.Add(new Category { CategoryId = ++i, Color = "#6666cc", IconId = "fa-wrench", Type = TransactionType.Expense, Name = SeedingDataText.Utilities });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#6666cc", IconId = "fa-wrench", CategoryId = i, Name = SeedingDataText.Utilities });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cc0000", IconId = "icon-voltage", CategoryId = i, Name = SeedingDataText.Electricity });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#cccc00", IconId = "icon-construction", CategoryId = i, Name = SeedingDataText.City });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#2db300", IconId = "fa-fire", CategoryId = i, Name = SeedingDataText.Gas });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#ff9900", IconId = "icon-phonealt", CategoryId = i, Name = SeedingDataText.HomePhone });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#009999", IconId = "icon-iphone", CategoryId = i, Name = SeedingDataText.MobilePhone });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#b300b3", IconId = "icon-circlefacebook", CategoryId = i, Name = SeedingDataText.Internet });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#992600", IconId = "icon-tv", CategoryId = i, Name = SeedingDataText.Television });
            SubCategoryList.Add(new SubCategory { SubCategoryId = ++y, Color = "#808000", IconId = "icon-watertap-plumbing", CategoryId = i, Name = SeedingDataText.Water });

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
