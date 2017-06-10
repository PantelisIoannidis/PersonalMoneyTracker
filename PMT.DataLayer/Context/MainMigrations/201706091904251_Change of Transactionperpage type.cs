namespace PMT.DataLayer.Context.MainMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeofTransactionperpagetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSettings", "TransactionsPerPage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSettings", "TransactionsPerPage", c => c.String());
        }
    }
}
