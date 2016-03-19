namespace JG.TimeLog.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using JG.TimeLog.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<JG.TimeLog.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JG.TimeLog.Web.Models.ApplicationDbContext context)
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
            //context.Projects.AddOrUpdate(p => p.Name,
            //    new Project
            //    {
            //        Name = "Webshop",
            //        Description = "Website for online commerce",
            //        Customer = new Customer
            //        {
            //            Name = "Amazing Customer",
            //            Address = "Great gade 1",
            //            City = "Copenhagen",
            //            Country = "Denmark",
            //            PostalCode = "1000",
            //            Email = "amazing@customer.com"
            //        }
            //    },

            //    new Project
            //    {
            //        Name = "System migration",
            //        Description = "Migration of systems",
            //        Customer = new Customer
            //        {
            //            Name = "Not So Amazing Customer",
            //            Address = "Great gade 2",
            //            City = "Copenhagen",
            //            Country = "Denmark",
            //            PostalCode = "1000",
            //            Email = "not.so.amazing@customer.com"
            //        }
            //    });
        }
    }
}
