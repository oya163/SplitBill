namespace SplitBill.Migrations
{
    using SplitBill.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SplitBill.Models.BillingDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SplitBill.Models.BillingDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Billings.AddOrUpdate(u => u.Title,
                new Billing
                {
                    Title = "Rent for Hartel",
                    PaidDate = DateTime.Parse("2015-05-11"),
                    PaidBy = "Oyesh Mann Singh",
                    PaidAmount = 150,
                    BillingMonth = "May",
                    BillingYear = 2015
                }
                );
        }
    }
}
