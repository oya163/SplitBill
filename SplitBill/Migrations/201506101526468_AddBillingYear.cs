namespace SplitBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBillingYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Billings", "BillingYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Billings", "BillingYear");
        }
    }
}
