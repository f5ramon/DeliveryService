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
    public class RoutesBusinessTest
    {
        [TestMethod]
        public void GetAll()
        {
            RoutesBusiness routesBiz = new RoutesBusiness(new UnitOfWork());

            // Arrange
            var result = routesBiz.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Route>));
        }

        [TestMethod]
        public void GetById()
        {
            RoutesBusiness routesBiz = new RoutesBusiness(new UnitOfWork());

            // Arrange
            var result = routesBiz.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Route));
            Assert.AreEqual(1, result.Id);
        }
    }
}
