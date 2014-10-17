using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_Application1.Models;
using System.Collections.Generic;
using P_Application1.Controllers;
using System.Web;
using System.IO;

namespace P_Application1.Tests.Controllers
{
    [TestClass]
    public class FileControllerTest
    {
        [TestMethod]
        //[DeploymentItem("Files/Input/sample_data.csv", "Files/Input/")]
        public void TestCase1()
        {
            // Arrange
            FileController controller = new FileController();

            // Act
            IList<District> result = controller.ReadFile("csv", "Antalya", "NoSort");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(83, result.Count);
        }

        [TestMethod]
        public void TestCase2()
        {
            // Arrange
            FileController controller = new FileController();

            // Act
            IList<District> result = controller.ReadFile("csv", "All", "CityName,DistrictName", "Asc");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3232, result.Count);
        }

        [TestMethod]
        public void TestCase3()
        {
            // Arrange
            FileController controller = new FileController();

            // Act
            IList<District> result = controller.ReadFile("xml", "Ankara", "ZipCode", "Desc");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(121, result.Count);
        }

        [TestMethod]
        public void TestCase4()
        {
            // Arrange
            FileController controller = new FileController();

            // Act
            IList<District> result = controller.ReadFile("xml", "All");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3232, result.Count);
        }
    }
}
