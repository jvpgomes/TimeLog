using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JG.TimeLog.Web.DataAccess;
using JG.TimeLog.Web.Models;
using JG.TimeLog.UnitTests.Helpers;

namespace JG.TimeLog.UnitTests.DataAccess
{
    /// <summary>
    /// Unit tests for data access functions.
    /// </summary>
    [TestClass]
    public class DataAccessTest
    {
        private TestDb testDb;
        private LocalMsSqlDb db;

        public DataAccessTest()
        {
            testDb = new TestDb();
            db = new LocalMsSqlDb(testDb.dbc);
        }
        
        [TestInitialize()]
        public void ResetData()
        {
            testDb.RestartDb();
        }

        [TestMethod]
        public void ReadDataTest()
        {
            testDb.AddTestDataset1();

            var customers = db.GetCustomersList();
            var projects = db.GetProjectsList();
            var timeEntries = db.GetTimeEntriesList();

            Assert.IsNotNull(customers);
            Assert.IsNotNull(projects);
            Assert.IsNotNull(timeEntries);
            Assert.AreEqual(2, customers.Count);
            Assert.AreEqual(2, projects.Count);
            Assert.AreEqual(6, timeEntries.Count);
            Assert.AreEqual("Amazing Customer", customers.First().Name);
            Assert.AreEqual("Webshop", projects.First().Name);
            Assert.AreEqual(10, timeEntries.First().Hours);

            var timeEntries1 = db.GetTimeEntriesListPerProject(1);
            var timeEntries2 = db.GetTimeEntriesListPerUser("gomes@mail.com");
            var timeEntries3 = db.GetTimeEntriesListPerUserPerProject("gomes@mail.com", 1);

            Assert.IsNotNull(timeEntries1);
            Assert.IsNotNull(timeEntries2);
            Assert.IsNotNull(timeEntries3);
            Assert.AreEqual(4, timeEntries1.Count);
            Assert.AreEqual(2, timeEntries2.Count);
            Assert.AreEqual(1, timeEntries3.Count);
            Assert.AreEqual(10, timeEntries1.First().Hours);
            Assert.AreEqual(6, timeEntries2.First().Hours);
            Assert.AreEqual(6, timeEntries3.First().Hours);
        }

        [TestMethod]
        public void InsertDataTest()
        {
            testDb.AddTestDataset1();

            var customer = new Customer
            {
                Id = 3,
                Name = "More or Less Ok Customer",
                Address = "Great gade 3",
                City = "Copenhagen",
                Country = "Denmark",
                PostalCode = "1000",
                Email = "ok@customer.com"
            };

            var project = new Project
            {
                Id = 3,
                Name = "Stuff",
                Description = "Amazing stuff",
                CustomerId = 3,
                Customer = customer
            };

            db.InsertCustomer(customer);

            db.InsertProject(project);

            db.InsertTimeEntry(new TimeEntry
            {
                Id = 7,
                ProjectId = 3,
                Project = project,
                Username = "gomes@mail.com",
                Date = new DateTime(2016, 3, 15),
                Hours = 1,
                AddedDateTime = new DateTimeOffset(2016, 3, 16, 9, 1, 0, new TimeSpan(1, 0, 0)),
                LastUpdatedDateTime = new DateTimeOffset(2016, 3, 16, 9, 1, 0, new TimeSpan(1, 0, 0))
            });

            var customers = db.GetCustomersList();
            var projects = db.GetProjectsList();
            var timeEntries = db.GetTimeEntriesList();

            Assert.IsNotNull(customers);
            Assert.IsNotNull(projects);
            Assert.IsNotNull(timeEntries);
            Assert.AreEqual(3, customers.Count);
            Assert.AreEqual(3, projects.Count);
            Assert.AreEqual(7, timeEntries.Count);
            Assert.AreEqual("More or Less Ok Customer", customers.Last().Name);
            Assert.AreEqual("Stuff", projects.Last().Name);
            Assert.AreEqual(1, timeEntries.Last().Hours);
        }

        [TestMethod]
        public void SelectEditDataTest()
        {
            testDb.AddTestDataset1();

            var customer = db.SelectCustomerFromId(1);
            var project = db.SelectProjectFromId(1);
            var timeEntry = db.SelectTimeEntryFromId(1);

            Assert.AreEqual("Amazing Customer", db.SelectCustomerFromId(1).Name);
            Assert.AreEqual("Webshop", db.SelectProjectFromId(1).Name);
            Assert.AreEqual(10, db.SelectTimeEntryFromId(1).Hours);

            customer.Name = "New Customer Name";
            project.Name = "New Project Name";
            timeEntry.Hours = 5;
            db.EditCustomer(customer);
            db.EditProject(project);
            db.EditTimeEntry(timeEntry);

            var customers = db.GetCustomersList();
            var projects = db.GetProjectsList();
            var timeEntries = db.GetTimeEntriesList();

            Assert.IsNotNull(customers);
            Assert.IsNotNull(projects);
            Assert.IsNotNull(timeEntries);
            Assert.AreEqual(2, customers.Count);
            Assert.AreEqual(2, projects.Count);
            Assert.AreEqual(6, timeEntries.Count);
            Assert.AreEqual("New Customer Name", customers.First().Name);
            Assert.AreEqual("New Project Name", projects.First().Name);
            Assert.AreEqual(5, timeEntries.First().Hours);
        }

        [TestMethod]
        public void DeleteDataTest()
        {
            testDb.AddTestDataset1();

            db.DeleteTimeEntry(1);
            var timeEntries = db.GetTimeEntriesList();
            Assert.IsNotNull(timeEntries);
            Assert.AreEqual(5, timeEntries.Count);
            Assert.AreEqual(5, timeEntries.First().Hours);

            db.DeleteProject(1);
            var projects = db.GetProjectsList();
            Assert.IsNotNull(projects);
            Assert.AreEqual(1, projects.Count);
            Assert.AreEqual("System migration", projects.First().Name);

            db.DeleteCustomer(1);
            var customers = db.GetCustomersList();
            Assert.IsNotNull(customers);
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual("Not So Amazing Customer", customers.First().Name);
        }
    }
}
