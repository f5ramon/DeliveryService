using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business.RouteSearchEngine
{
    public class RouteSearchBFS : BaseRouteSearch, IRouteSearcher
    {
        public List<List<Route>> SearchAllRoutes(int originPointId, int destinationPointId)
        {
            throw new Exception("not implemented");
        }

        public List<List<Route>> SearchAllRoutes(Point originPoint, Point destinationPoint)
        {
            throw new Exception("not implemented");
        }
    }
}
