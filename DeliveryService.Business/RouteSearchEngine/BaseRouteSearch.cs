using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business.RouteSearchEngine
{
    public abstract class BaseRouteSearch
    {
        protected List<Point> _allPointsAvailable;
        protected int _minimumIntermediatePoints;

        public void LoadAllPoints(List<Point> allPointsAvailable)
        {
            _allPointsAvailable = allPointsAvailable;
        }

        public void SetMinimumIntermediatePoints(int minumumIntermedatePoints)
        {
            _minimumIntermediatePoints = minumumIntermedatePoints;
        }

        public int GetMinimumIntermediatePoints()
        {
            return _minimumIntermediatePoints;
        }

        protected Point GetPoint(int id)
        {
            return _allPointsAvailable.FirstOrDefault(x => x.Id == id);
        }
        
        protected bool AvailablePointsNotLoaded()
        {
            return _allPointsAvailable == null || !_allPointsAvailable.Any();
        }
    }
}
