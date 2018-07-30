namespace DeliveryService.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginPointId = c.Int(nullable: false),
                        DestinationPointId = c.Int(nullable: false),
                        Time = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.DestinationPointId, cascadeDelete: false)
                .ForeignKey("dbo.Points", t => t.OriginPointId, cascadeDelete: false)
                .Index(t => t.OriginPointId)
                .Index(t => t.DestinationPointId);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "OriginPointId", "dbo.Points");
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.Points");
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropIndex("dbo.Routes", new[] { "OriginPointId" });
            DropTable("dbo.Points");
            DropTable("dbo.Routes");
        }
    }
}
