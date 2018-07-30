using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business.RouteSearchEngine
{
    public class SearchEngine
    {
        private IRouteSearcher _routeSearcher;

        public SearchEngine(RouteSearcherType routeSearchEngineType)
        {
            switch (routeSearchEngineType)
            {
                case RouteSearcherType.DFS:
                    _routeSearcher = (IRouteSearcher)new RouteSearchDFS();
                    break;
                case RouteSearcherType.BFS:
                    _routeSearcher = (IRouteSearcher)new RouteSearchDFS();
                    break;
            }
            
        }

        public void LoadAllPoints(List<Point> allPointsAvailable)
        {
            ((BaseRouteSearch)_routeSearcher).LoadAllPoints(allPointsAvailable);
        }

        public void SetMinimumIntermediatePoints(int minimumIntermediatePoints)
        {
            ((BaseRouteSearch)_routeSearcher).SetMinimumIntermediatePoints(minimumIntermediatePoints);
        }

        public int GetMinimumIntermediatePoints()
        {
            return ((BaseRouteSearch)_routeSearcher).GetMinimumIntermediatePoints();
        }

        public List<List<Route>> SearchRoutes(int originPointId, int destinationPointId)
        {
            return _routeSearcher.SearchAllRoutes(originPointId, destinationPointId);
        }

        public List<List<Route>> SearchRoutes(Point originPoint, Point destinationPoint)
        {
            return _routeSearcher.SearchAllRoutes(originPoint, destinationPoint);
        }

        public IRouteSearcher GetSearcher()
        {
            return _routeSearcher;
        }
    }
}
