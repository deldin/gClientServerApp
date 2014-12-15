using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiServer;
using ApiServer.Controllers;

namespace ApiServer.Tests.Controllers
{
    [TestClass]
    public class ServerControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            CompilerController controller = new CompilerController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            CompilerController controller = new CompilerController();

            // Act
            //string result = controller.Get(5);

            // Assert
           // Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            CompilerController controller = new CompilerController();

            // Act
           // controller.Post();

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            CompilerController controller = new CompilerController();

            // Act
            controller.Put();

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            CompilerController controller = new CompilerController();

            // Act
            controller.Delete();

            // Assert
        }
    }
}
