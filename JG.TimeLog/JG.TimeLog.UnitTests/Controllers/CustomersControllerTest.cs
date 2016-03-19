using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JG.TimeLog.Web.Controllers;

namespace JG.TimeLog.UnitTests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        [TestMethod]
        public void ReturnViewTest()
        {
            var controller = new CustomersController();
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
