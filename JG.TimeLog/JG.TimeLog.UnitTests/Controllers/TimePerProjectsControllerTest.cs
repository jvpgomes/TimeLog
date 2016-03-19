using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JG.TimeLog.Web.Controllers;
using JG.TimeLog.UnitTests.Helpers;
using JG.TimeLog.Web.DataAccess;
using System.Linq;
using System.Collections.Generic;
using JG.TimeLog.Web.Models;

namespace JG.TimeLog.UnitTests.Controllers
{
    [TestClass]
    public class TimePerProjectsControllerTest
    {
        private TestDb testDb;
        private LocalMsSqlDb db;

        public TimePerProjectsControllerTest()
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
        public void TotalTimePerProjectTest()
        {
            testDb.AddTestDataset1();

            var controller1 = new MonthlyTimePerProjectsController();
            var result1 = controller1.IndexTotal() as ViewResult;

            var controller2 = new TimePerProjectsController();
            var result2 = controller2.IndexTotal() as ViewResult;

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);

            var model1 = (IEnumerable<MonthlyTimePerProject>)result1.Model;
            Assert.AreEqual(21, model1.First().TotalTime);

            var model2 = (IEnumerable<TimePerProject>)result2.Model;
            Assert.AreEqual(28, model2.First().TotalTime);
        }

        [TestMethod]
        public void TotalTimePerCustomerTest()
        {
            testDb.AddTestDataset1();

            var controller1 = new MonthlyTimePerCustomersController();
            var result1 = controller1.IndexTotal() as ViewResult;

            var controller2 = new TimePerCustomersController();
            var result2 = controller2.IndexTotal() as ViewResult;

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);

            var model1 = (IEnumerable<MonthlyTimePerCustomer>)result1.Model;
            Assert.AreEqual(21, model1.First().TotalTime);

            var model2 = (IEnumerable<TimePerCustomer>)result2.Model;
            Assert.AreEqual(28, model2.First().TotalTime);
        }
    }
}
