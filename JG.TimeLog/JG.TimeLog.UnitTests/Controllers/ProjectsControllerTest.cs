using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JG.TimeLog.Web.Controllers;

namespace JG.TimeLog.UnitTests.Controllers
{
    [TestClass]
    public class ProjectsControllerTest
    {
        [TestMethod]
        public void ReturnViewTest()
        {
            var controller = new ProjectsController();
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
