using DeliveryService.Business;
using DeliveryService.Business.RouteSearchEngine;
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
    public class RouteSearch
    {
        [TestMethod]
        public void FactoryTest()
        {
            SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

            // Assert
            Assert.IsNotNull(searchEngine);
            Assert.IsInstanceOfType(searchEngine.GetSearcher(), typeof(IRouteSearcher));
            Assert.IsInstanceOfType(searchEngine.GetSearcher(), typeof(BaseRouteSearch));
            Assert.IsInstanceOfType(searchEngine.GetSearcher(), typeof(RouteSearchDFS));
            Assert.IsNotInstanceOfType(searchEngine.GetSearcher(), typeof(RouteSearchBFS));
        }

        [TestMethod]
        public void RouteSearchTest()
        {
            PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());
            SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

            searchEngine.LoadAllPoints(pointsBiz.GetAll());

            var routeListUsingIds = searchEngine.SearchRoutes(1, 6);
            var routeListUsingObjects = searchEngine.SearchRoutes(pointsBiz.GetById(1), pointsBiz.GetById(6));

            // Assert
            Assert.IsNotNull(routeListUsingIds);
            Assert.IsNotNull(routeListUsingObjects);
            Assert.AreEqual(routeListUsingObjects.Count, routeListUsingIds.Count);
        }

        [TestMethod]
        public void RouteSearchWithMinmumIntermediateTest()
        {
            PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());
            SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

            var minimumIntermediatePoints = 1;

            searchEngine.SetMinimumIntermediatePoints(minimumIntermediatePoints);
            searchEngine.LoadAllPoints(pointsBiz.GetAll());

            var routeList = searchEngine.SearchRoutes(3, 6);

            // Assert
            Assert.IsNotNull(routeList);
            Assert.IsTrue(searchEngine.GetMinimumIntermediatePoints() == minimumIntermediatePoints);
            Assert.IsTrue(routeList.Count > minimumIntermediatePoints);
        }


        [TestMethod]
        public void RouteSearchExceptionThrown()
        {
            try
            {
                PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());
                SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

                var routeList = searchEngine.SearchRoutes(3, 6);
            }
            catch (Exception)
            {
                Assert.IsTrue(1 == 1);
            }
        }

        [TestMethod]
        public void RouteSearchExceptionPointsNotLoadedThrown()
        {
            try
            {
                PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());
                SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

                var routeList = searchEngine.SearchRoutes(3, 6);
            }
            catch (Exception ex)
            {
                // not the right kind of exception
                Assert.IsTrue(ex.Message == "Available points not loaded yet.");
            }
        }

        [TestMethod]
        public void RouteSearchExceptionOriginNotFound()
        {
            try
            {
                PointsBusiness pointsBiz = new PointsBusiness(new UnitOfWork());
                SearchEngine searchEngine = new SearchEngine(RouteSearcherType.DFS);

                var minimumIntermediatePoints = 1;

                searchEngine.SetMinimumIntermediatePoints(minimumIntermediatePoints);
                searchEngine.LoadAllPoints(pointsBiz.GetAll());

                var routeList = searchEngine.SearchRoutes(9, 6);
            }
            catch (Exception ex)
            {
                // not the right kind of exception
                Assert.IsTrue(ex.Message == "Origin point not found");
            }
        }

    }
}
