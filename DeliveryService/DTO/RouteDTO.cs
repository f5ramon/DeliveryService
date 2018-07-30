using DeliveryService.Business;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.DTO
{
    public class RouteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "OriginalPointId is required!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number")]
        public int OriginPointId { get; set; }

        [Required(ErrorMessage = "DestinationPointId is required!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number")]
        public int DestinationPointId { get; set; }

        [Required(ErrorMessage = "Time is required!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number")]
        public int Time { get; set; }

        [Required(ErrorMessage = "Cost is required!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number")]
        public int Cost { get; set; }

        public Route ToRoute()
        {
            return new Route()
            {
                Id = Id,
                OriginPointId = OriginPointId,
                DestinationPointId = DestinationPointId,
                Time = Time,
                Cost = Cost
            };
        }

        public Route ToRouteUpdate(RoutesBusiness routesBiz)
        {
            var route = routesBiz.GetByIdAttached(this.Id);
            route.OriginPointId = this.OriginPointId;
            route.DestinationPointId = this.DestinationPointId;
            route.Time = this.Time;
            route.Cost = this.Cost;

            return route;
        }

        public static RouteDTO ToDTO(Route route)
        {
            return new RouteDTO
            {
                Id = route.Id,
                OriginPointId = route.OriginPointId,
                DestinationPointId = route.DestinationPointId,
                Cost = route.Cost,
                Time = route.Time
            };
        }
    }
}