using PMT.DataLayer.Context;
using PMT.Entities;
using PMT.DataLayer.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer
{
    public class MainDb : DbContext
    {
        public MainDb()
            :base("name=DefaultConnection")
        {
            #if DEBUG
                Database.Log = msg => Debug.WriteLine(msg);
            #endif
            Database.SetInitializer(new MainDbInitializer());
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Repeat> Repeats { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MoneyAccount> MoneyAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Budget>().HasKey<int>(e => e.BudgetId);
            modelBuilder.Entity<Category>().HasKey<int>(e => e.CategoryId);
            modelBuilder.Entity<Repeat>().HasKey<int>(e => e.RepeatId);
            modelBuilder.Entity<SubCategory>().HasKey<int>(e => e.SubCategoryId);
            modelBuilder.Entity<Transaction>().HasKey<int>(e => e.TransactionId);
            modelBuilder.Entity<MoneyAccount>().HasKey<int>(e => e.MoneyAccountId);

            modelBuilder.Entity<Budget>().Property(e => e.BudgetId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Category>().Property(e => e.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Icon>().Property(e => e.IconId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Repeat>().Property(e => e.RepeatId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SubCategory>().Property(e => e.SubCategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Transaction>().Property(e => e.TransactionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MoneyAccount>().Property(e => e.MoneyAccountId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // We ignore these fields because we want to avoid using seperate modelviews for the simplest occutions
            modelBuilder.Entity<MoneyAccount>().Ignore(e => e.Balance);

            modelBuilder.Entity<Budget>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            

            base.OnModelCreating(modelBuilder);
        }

        public class MainDbInitializer : CreateDatabaseIfNotExists<MainDb>
        {
            protected override void Seed(MainDb context)
            {
                new Seeding().Seed(context);
                base.Seed(context);
            }
        }


    }
}
