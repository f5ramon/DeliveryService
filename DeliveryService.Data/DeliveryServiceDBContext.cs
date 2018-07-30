using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Models;
using DeliveryService.Mappings;

namespace DeliveryService.Data
{
    public class DeliveryServiceDBContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new PointMap());
        }
        public DeliveryServiceDBContext() : base("DeliveryServiceConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<DeliveryServiceDBContext>(null);

        }
    }
}

