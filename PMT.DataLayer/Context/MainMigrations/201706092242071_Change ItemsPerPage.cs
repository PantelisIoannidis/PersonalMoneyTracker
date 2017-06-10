namespace PMT.DataLayer.Context.MainMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeItemsPerPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "ItemsPerPage", c => c.Int(nullable: false));
            DropColumn("dbo.UserSettings", "TransactionsPerPage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSettings", "TransactionsPerPage", c => c.Int(nullable: false));
            DropColumn("dbo.UserSettings", "ItemsPerPage");
        }
    }
}
