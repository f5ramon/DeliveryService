using DeliveryService.Business;
using DeliveryService.Data;
using DeliveryService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Tests.Business
{
    [TestClass]
    public class PointsBusinessTest
    {
        [TestMethod]
        public void GetAll()
        {
            PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());

            // Arrange
            var result = pointsBiz.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Point>));
        }

        [TestMethod]
        public void GetById()
        {
            PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());

            // Arrange
            var result = pointsBiz.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Point));
            Assert.AreEqual(1, result.Id);
        }
    }
}
