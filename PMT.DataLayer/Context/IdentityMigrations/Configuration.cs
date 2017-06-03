namespace PMT.DataLayer.Context.IdentityMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PMT.DataLayer.Seed;

    internal sealed class Configuration : DbMigrationsConfiguration<PMT.DataLayer.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Context\IdentityMigrations";
        }

        protected override void Seed(PMT.DataLayer.IdentityDb context)
        {
            new CommonSeeding().Seed(context);
        }
    }
}
