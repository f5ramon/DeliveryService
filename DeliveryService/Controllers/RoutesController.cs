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
using DeliveryService.DTO;
using DeliveryService.ExtendedAttributes;
using DeliveryService.Models;

namespace DeliveryService.Controllers
{
    [ControllerExceptionFilter]
    public class RoutesController : ApiController
    {
        private readonly RoutesBusiness _routesBiz;

        public RoutesController(RoutesBusiness routesBusiness)
        {
            _routesBiz = routesBusiness;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_routesBiz.GetAllActive());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(_routesBiz.GetById(id));
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Add([FromBody] RouteDTO routeDTO)
        {
            var route = routeDTO.ToRoute();
            _routesBiz.Create(route);
            return Created(string.Format("{0}/{1}", Request.RequestUri, route.Id), route);
        }

        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update([FromBody] RouteDTO routeDTO)
        {
            var route = routeDTO.ToRouteUpdate(_routesBiz);
            _routesBiz.Update(route);
            return Ok(_routesBiz.GetById(route.Id));
        }

        public IHttpActionResult Delete(int id)
        {
            _routesBiz.DeleteById(id);
            return Ok(new { result = "Successfully deleted" });
        }
    }
}