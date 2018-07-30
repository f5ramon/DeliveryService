using DeliveryService.Business;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.DTO
{
    public class PointDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Point ToPoint()
        {
            return new Point() {
                Id = Id,
                Name = Name
            };
        }

        public Point ToPointUpdate(PointsBusiness pointsBiz)
        {
            var point = pointsBiz.GetByIdAttached(this.Id);
            point.Name = this.Name;
            return point;
        }

        public static PointDTO ToDTO(Point point)
        {
            return new PointDTO
            {
                Id = point.Id,
                Name = point.Name
            };
        }
    }
}