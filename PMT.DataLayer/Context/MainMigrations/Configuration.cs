namespace PMT.DataLayer.Context.MainMigrations
{
    using PMT.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PMT.DataLayer.Seed;

    internal sealed class Configuration : DbMigrationsConfiguration<PMT.DataLayer.MainDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Context\MainMigrations";
        }

        protected override void Seed(PMT.DataLayer.MainDb context)
        {
            new Seeding().Seed(context);
        }
    }
}
