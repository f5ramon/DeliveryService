using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public class RoutesRepository : GenericRepository<Route>
    {
        public RoutesRepository(DeliveryServiceDBContext db) : base(db)
        {
            //NestedProperties = new[] { "X", "Y", "Z" };
        }
    }
}
