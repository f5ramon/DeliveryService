using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Models;

namespace DeliveryService.Mappings
{
    public class PointMap : EntityTypeConfiguration<Point>
    {
        public PointMap()
        {
            this.HasKey(x => x.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.ToTable("dbo.Points");
        }
    }
}