using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business.RouteSearchEngine
{
    public interface IRouteSearcher
    {
        List<List<Route>> SearchAllRoutes(int originPointId, int destinationPointId);

        List<List<Route>> SearchAllRoutes(Point originPoint, Point destinationPoint);
    }
}
