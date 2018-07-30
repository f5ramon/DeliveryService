namespace DeliveryService.Data.Migrations
{
    using DeliveryService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DeliveryService.Data.DeliveryServiceDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DeliveryService.Data.DeliveryServiceDBContext context)
        {
            List<Point> allPoints = new List<Point>();

            context.Points.AddOrUpdate(x => x.Id,
                new Point() { Id = 1, Name = "1", Active = true },
                new Point() { Id = 2, Name = "2", Active = true },
                new Point() { Id = 3, Name = "3", Active = true },
                new Point() { Id = 4, Name = "4", Active = true },
                new Point() { Id = 5, Name = "5", Active = true },
                new Point() { Id = 6, Name = "6", Active = true }
            );

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 1, OriginPointId = 1, DestinationPointId = 2, Cost = 2, Time = 10, Active = true },
                new Route() { Id = 2, OriginPointId = 1, DestinationPointId = 5, Cost = 4, Time = 20, Active = true },
                new Route() { Id = 3, OriginPointId = 1, DestinationPointId = 6, Cost = 6, Time = 15, Active = true });

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 4, OriginPointId = 2, DestinationPointId = 3, Cost = 2, Time = 10, Active = true },
                new Route() { Id = 5, OriginPointId = 2, DestinationPointId = 4, Cost = 1, Time = 20, Active = true });

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 6, OriginPointId = 3, DestinationPointId = 5, Cost = 1, Time = 20, Active = true },
                new Route() { Id = 7, OriginPointId = 3, DestinationPointId = 6, Cost = 5, Time = 30, Active = true });

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 8, OriginPointId = 4, DestinationPointId = 3, Cost = 1, Time = 10, Active = true },
                new Route() { Id = 9, OriginPointId = 4, DestinationPointId = 6, Cost = 2, Time = 10, Active = true });

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 10, OriginPointId = 5, DestinationPointId = 2, Cost = 5, Time = 30, Active = true },
                new Route() { Id = 11, OriginPointId = 5, DestinationPointId = 6, Cost = 2, Time = 10, Active = true });

            context.Routes.AddOrUpdate(x => x.Id,
                new Route() { Id = 12, OriginPointId = 6, DestinationPointId = 4, Cost = 3, Time = 5, Active = true });

            context.SaveChanges();
        }
    }
}
