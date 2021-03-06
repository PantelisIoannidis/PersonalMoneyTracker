﻿using PMT.Entities;
using PMT.DataLayer.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PMT.DataLayer
{
    public class MainDb : IdentityDbContext<ApplicationUser>
    {
        public MainDb()
            :base(Literals.ConnectionStrings.MainConnectionStringName)
        {
            #if DEBUG
                Database.Log = msg => Debug.WriteLine(msg);
            #endif

            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MainDbInitializer());
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MoneyAccount> MoneyAccounts { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>().HasKey<int>(e => e.TransactionId);
            modelBuilder.Entity<MoneyAccount>().HasKey<int>(e => e.MoneyAccountId);

            modelBuilder.Entity<Category>().Property(e => e.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SubCategory>().Property(e => e.SubCategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Transaction>().Property(e => e.TransactionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MoneyAccount>().Property(e => e.MoneyAccountId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // We ignore these fields because we want to avoid using seperate modelviews for the simplest occutions
            modelBuilder.Entity<MoneyAccount>().Ignore(e => e.Balance);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            base.OnModelCreating(modelBuilder);
        }

        public class MainDbInitializer : CreateDatabaseIfNotExists<MainDb>
        {
            protected override void Seed(MainDb context)
            {
                new CommonSeeding().Seed(context);
                base.Seed(context);
            }
        }

        public static MainDb Create()
        {
            return new MainDb();
        }

    }
}
