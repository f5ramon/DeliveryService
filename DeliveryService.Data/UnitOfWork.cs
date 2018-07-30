using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Data.Repositories;

namespace DeliveryService.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public DeliveryServiceDBContext DbContext;

        public PointsRepository Points { get; set; }
        public RoutesRepository Routes { get; set; }

        public UnitOfWork()
        {
            //DbContext = new DeliveryServiceDBContext();
            //this.DbContext.Database.CommandTimeout = 0;
            this.DbContext = new DeliveryServiceDBContext();
            InitializeRepositories();
        }
        public void InitializeRepositories()
        {

            Points = new PointsRepository(DbContext);
            Routes = new RoutesRepository(DbContext);

        }
        public T GetRepository<T>()
        {
            return (T)this.GetType().GetProperties().SingleOrDefault(x => x.PropertyType == typeof(T) || typeof(T).IsAssignableFrom(x.PropertyType)).GetValue(this);
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
