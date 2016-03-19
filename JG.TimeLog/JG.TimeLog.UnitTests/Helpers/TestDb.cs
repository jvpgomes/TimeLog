using System;
using JG.TimeLog.Web.Models;

namespace JG.TimeLog.UnitTests.Helpers
{
    public class TestDb 
    {
        public ApplicationDbContext dbc { get; set; }

        public TestDb()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            dbc = new ApplicationDbContext();

            if (dbc.Database.Exists())
            {
                dbc.Database.Delete();
            }
            dbc.Database.Initialize(true);
        }
        
        public void RestartDb()
        {
            dbc.Database.Delete();
            dbc.Database.Initialize(true);
        }

        public void AddTestDataset1()
        {
            var customer1 = new Customer
            {
                Id = 1,
                Name = "Amazing Customer",
                Address = "Great gade 1",
                City = "Copenhagen",
                Country = "Denmark",
                PostalCode = "1000",
                Email = "amazing@customer.com"
            };

            var customer2 = new Customer
            {
                Id = 2,
                Name = "Not So Amazing Customer",
                Address = "Great gade 2",
                City = "Copenhagen",
                Country = "Denmark",
                PostalCode = "1000",
                Email = "not.so.amazing@customer.com"
            };

            var project1 = new Project
            {
                Id = 1,
                Name = "Webshop",
                Description = "Website for online commerce",
                CustomerId = 1,
                Customer = customer1
            };

            var project2 = new Project
            {
                Id = 2,
                Name = "System migration",
                Description = "Migration of systems",
                CustomerId = 2,
                Customer = customer2
            };

            
            dbc.Customers.Add(customer1);
            dbc.Customers.Add(customer2);
            dbc.Projects.Add(project1);
            dbc.Projects.Add(project2);

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 1,
                ProjectId = 1,
                Project = project1,
                Username = "joao@mail.com",
                Date = new DateTime(2016, 3, 15),
                Hours = 10,
                AddedDateTime = new DateTimeOffset(2016, 3, 16, 9, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 16, 9, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 2,
                ProjectId = 1,
                Project = project1,
                Username = "joao@mail.com",
                Date = new DateTime(2016, 3, 14),
                Hours = 5,
                AddedDateTime = new DateTimeOffset(2016, 3, 15, 9, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 16, 9, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 3,
                ProjectId = 1,
                Project = project1,
                Username = "joao@mail.com",
                Date = new DateTime(2016, 2, 14),
                Hours = 7,
                AddedDateTime = new DateTimeOffset(2016, 2, 15, 9, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 2, 16, 9, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 4,
                ProjectId = 2,
                Project = project2,
                Username = "joao@mail.com",
                Date = new DateTime(2016, 3, 14),
                Hours = 2,
                AddedDateTime = new DateTimeOffset(2016, 3, 15, 10, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 15, 10, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 5,
                ProjectId = 1,
                Project = project1,
                Username = "gomes@mail.com",
                Date = new DateTime(2016, 3, 14),
                Hours = 6,
                AddedDateTime = new DateTimeOffset(2016, 3, 15, 10, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 15, 10, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.TimeEntries.Add(new TimeEntry
            {
                Id = 6,
                ProjectId = 1,
                Project = project2,
                Username = "gomes@mail.com",
                Date = new DateTime(2016, 3, 12),
                Hours = 6,
                AddedDateTime = new DateTimeOffset(2016, 3, 13, 10, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 13, 10, 1, 0, new TimeSpan(1, 0, 0))
            });

            dbc.SaveChanges();
        }
    }
}
