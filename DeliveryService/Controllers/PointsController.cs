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
using DeliveryService.ExtendedAttributes;
using DeliveryService.Business;
using DeliveryService.DTO;

namespace DeliveryService.Controllers
{
    [ControllerExceptionFilter]
    public class PointsController : ApiController
    {
        private readonly PointsBusiness _pointsBiz;

        public PointsController(PointsBusiness pointsBusiness)
        {
            _pointsBiz = pointsBusiness;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_pointsBiz.GetAllActive());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(_pointsBiz.GetById(id));
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Add([FromBody] PointDTO pointDTO)
        {
            var point = pointDTO.ToPoint();
            _pointsBiz.Create(point);
            return Created(string.Format("{0}/{1}", Request.RequestUri, point.Id), point);
        }

        [HttpPut]
        [ValidateModel]
        public IHttpActionResult Update([FromBody] PointDTO pointDTO)
        {
            var point = pointDTO.ToPointUpdate(_pointsBiz);
            _pointsBiz.Update(point);
            return Ok(_pointsBiz.GetById(point.Id));
        }

        public IHttpActionResult Delete(int id)
        {
            _pointsBiz.DeleteById(id);
            return Ok(new { result = "Successfully deleted" });
        }
    }
}