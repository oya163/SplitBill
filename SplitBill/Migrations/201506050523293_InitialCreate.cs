namespace SplitBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PaidDate = c.DateTime(nullable: false),
                        PaidBy = c.String(),
                        PaidAmount = c.Double(nullable: false),
                        BillingMonth = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PaidDate = c.DateTime(nullable: false),
                        PaidBy = c.String(),
                        PaidAmount = c.Double(nullable: false),
                        BillingMonth = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
            DropTable("dbo.Billings");
        }
    }
}
