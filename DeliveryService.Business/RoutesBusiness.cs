using DeliveryService.Data;
using DeliveryService.Data.Repositories;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Business
{
    public class RoutesBusiness : BaseBusiness<GenericRepository<Route>, Route>
    {
        public RoutesRepository ConcreteRepository { get; private set; }

        public RoutesBusiness(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ConcreteRepository = (RoutesRepository)Repository;
        }
    }
}
