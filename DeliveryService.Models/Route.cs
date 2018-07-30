using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Route : BaseModel
    {
        [ForeignKey("OriginPoint")]
        public int OriginPointId { get; set; }
        [ForeignKey("DestinationPoint")]
        public int DestinationPointId { get; set; }
        public int Time { get; set; }
        public int Cost { get; set; }

        [InverseProperty("DepartureRoutes")]
        public virtual Point OriginPoint { get; set; }
        [InverseProperty("ArrivalRoutes")]
        public virtual Point DestinationPoint { get; set; }
    }
}
