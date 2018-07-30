using DeliveryService.Models;
using DeliveryService.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryService.Business.RouteSearchEngine;

namespace DeliveryService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            PointsBusiness pointsBiz = new PointsBusiness(new Data.UnitOfWork());

            var allPoints = pointsBiz.GetAll();

            var searchEngine = new SearchEngine(RouteSearcherType.DFS);

            searchEngine.LoadAllPoints(allPoints);

            var routes = searchEngine.SearchRoutes(allPoints[0], allPoints[5]);

            return View();
        }
    }
}
