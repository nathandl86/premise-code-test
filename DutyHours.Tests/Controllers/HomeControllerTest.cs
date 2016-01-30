using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DutyHours.Controllers;

namespace DutyHours.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Duty Hours | Home", result.ViewBag.Title);
        }

        [TestMethod]
        public void ResidentCalendar()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = controller.ResidentCalendar() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Duty Hours | Resident Calendar", result.ViewBag.Title);
        }

        [TestMethod]
        public void Analysis()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Analysis() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Duty Hours | Analysis", result.ViewBag.Title);
        }
    }
}
