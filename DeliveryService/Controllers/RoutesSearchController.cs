using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DeliveryService.Business;
using DeliveryService.Business.RouteSearchEngine;
using DeliveryService.DTO;
using DeliveryService.ExtendedAttributes;
using DeliveryService.Models;

namespace DeliveryService.Controllers
{
    [ControllerExceptionFilter]
    public class RouteSearchController : ApiController
    {
        private readonly RoutesBusiness _routesBiz;
        private readonly PointsBusiness _pointsBiz;

        public RouteSearchController(RoutesBusiness routesBusiness, PointsBusiness pointsBusiness)
        {
            _routesBiz = routesBusiness;
            _pointsBiz = pointsBusiness;
        }

        [Route("api/SearchRoutes/{originPointId:int}/{destinationPointId:int}")]
        [HttpGet]
        public IHttpActionResult SearchRoutes(int originPointId, int destinationPointId)
        {
            var searchEngine = new SearchEngine(RouteSearcherType.DFS);
            searchEngine.LoadAllPoints(_pointsBiz.GetAll());
            var routeList = searchEngine.SearchRoutes(originPointId, destinationPointId);

            var result = routeList.Select(
                    x=>x.Select(y => RouteDTO.ToDTO(y))
                );

            return Ok(result);
        }
    }
}