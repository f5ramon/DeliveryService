using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business.RouteSearchEngine
{
    public class RouteSearchDFS : BaseRouteSearch, IRouteSearcher
    {
        public List<List<Route>> SearchAllRoutes(int originPointId, int destinationPointId)
        {
            if (AvailablePointsNotLoaded())
                throw new Exception("Available points not loaded yet.");

            Point originPoint = GetPoint(originPointId);
            if (originPoint == null)
                throw new Exception("Origin point not found");

            Point destinationPoint = GetPoint(destinationPointId);
            if (destinationPoint == null)
                throw new Exception("destination point not found");

            return SearchAllRoutes(originPoint, destinationPoint);
        }

        public List<List<Route>> SearchAllRoutes(Point originPoint, Point destinationPoint)
        {
            if (AvailablePointsNotLoaded())
                throw new Exception("Available points not loaded yet.");

            HashSet<Int32> visitedPointIds = new HashSet<int>();
            HashSet<Route> localRoutes = new HashSet<Route>();
            List<List<Route>> resultRoutes = new List<List<Route>>();

            LoadRoutes(originPoint, destinationPoint, visitedPointIds, localRoutes, resultRoutes);

            return resultRoutes;
        }

        private void LoadRoutes(Point origin, Point destination, HashSet<Int32> visitedPointIds, HashSet<Route> localRoutes, List<List<Route>> resultRoutes)
        {
            visitedPointIds.Add(origin.Id);

            if (origin.Id == destination.Id)
            {
                if (localRoutes.Count > _minimumIntermediatePoints)
                    resultRoutes.Add(localRoutes.Select(x => x).ToList());
            }

            if(origin.DepartureRoutes!=null)
            {
                var destinationsList = origin.DepartureRoutes.Where(x => !visitedPointIds.Contains(x.DestinationPointId));

                foreach (var route in destinationsList)
                {
                    localRoutes.Add(route);
                    LoadRoutes(GetPoint(route.DestinationPointId), destination, visitedPointIds, localRoutes, resultRoutes);
                    localRoutes.Remove(route);
                }
            }

            visitedPointIds.Remove(origin.Id);
        }
    }
}
