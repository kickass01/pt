namespace PinkTravel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        HotelName = c.String(nullable: false),
                        LocationName = c.String(nullable: false),
                        HotelImageId = c.Int(nullable: false),
                        LocationImageId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OfferId)
                .ForeignKey("dbo.ImageModels", t => t.HotelImageId, cascadeDelete: false)
                .ForeignKey("dbo.ImageModels", t => t.LocationImageId, cascadeDelete: false)
                .Index(t => t.HotelImageId)
                .Index(t => t.LocationImageId);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullImageName = c.String(nullable: false),
                        CroppedImageName = c.String(),
                        ContentType = c.String(),
                        CroppedImageSize = c.Int(nullable: false),
                        FullImageSize = c.Int(nullable: false),
                        CropX1 = c.Int(nullable: false),
                        CropX2 = c.Int(nullable: false),
                        CropY1 = c.Int(nullable: false),
                        CropY2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExternalUserInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FullName = c.String(),
                        Verified = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Offers", new[] { "LocationImageId" });
            DropIndex("dbo.Offers", new[] { "HotelImageId" });
            DropForeignKey("dbo.Offers", "LocationImageId", "dbo.ImageModels");
            DropForeignKey("dbo.Offers", "HotelImageId", "dbo.ImageModels");
            DropTable("dbo.ExternalUserInformations");
            DropTable("dbo.ImageModels");
            DropTable("dbo.Offers");
        }
    }
}
