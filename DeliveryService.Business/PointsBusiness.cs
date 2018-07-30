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
    public class PointsBusiness : BaseBusiness<GenericRepository<Point>, Point>
    {
        public PointsRepository ConcreteRepository { get; private set; }

        public PointsBusiness(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ConcreteRepository = (PointsRepository)Repository;
        }
    }
}
