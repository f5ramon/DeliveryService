using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Point : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Route> DepartureRoutes { get; set; }
        public virtual ICollection<Route> ArrivalRoutes { get; set; }
    }
}
