using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public class PointsRepository : GenericRepository<Point>
    {
        public PointsRepository(DeliveryServiceDBContext db) : base(db)
        {
            NestedProperties = new[] { "DepartureRoutes", "ArrivalRoutes" };
        }
    }
}
