namespace SplitBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        monthId = c.Int(nullable: false, identity: true),
                        month = c.String(),
                    })
                .PrimaryKey(t => t.monthId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Months");
        }
    }
}
