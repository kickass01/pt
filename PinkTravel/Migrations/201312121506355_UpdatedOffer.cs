namespace PinkTravel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOffer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "HotelImageId", "dbo.ImageModels");
            DropForeignKey("dbo.Offers", "LocationImageId", "dbo.ImageModels");
            DropIndex("dbo.Offers", new[] { "HotelImageId" });
            DropIndex("dbo.Offers", new[] { "LocationImageId" });
            AddColumn("dbo.Offers", "Location", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "Country", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Offers", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Offers", "PriceFrom", c => c.Double(nullable: false));
            AddColumn("dbo.Offers", "HotelStars", c => c.Int(nullable: false));
            AddColumn("dbo.Offers", "HotelWebSite", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "HotelDescription", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "TransportDetails", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "TransferDetails", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "RoomDetails", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "MealDetails", c => c.String(nullable: false));
            AddColumn("dbo.Offers", "Observations", c => c.String());
            AddColumn("dbo.Offers", "NotIncludedInOffer", c => c.String());
            AlterColumn("dbo.Offers", "HotelName", c => c.String(nullable: false));
            AlterColumn("dbo.Offers", "HotelImageId", c => c.Int());
            AlterColumn("dbo.Offers", "LocationImageId", c => c.Int());
            AddForeignKey("dbo.Offers", "HotelImageId", "dbo.ImageModels", "Id");
            AddForeignKey("dbo.Offers", "LocationImageId", "dbo.ImageModels", "Id");
            CreateIndex("dbo.Offers", "HotelImageId");
            CreateIndex("dbo.Offers", "LocationImageId");
            DropColumn("dbo.Offers", "LocationName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offers", "LocationName", c => c.String(nullable: false));
            DropIndex("dbo.Offers", new[] { "LocationImageId" });
            DropIndex("dbo.Offers", new[] { "HotelImageId" });
            DropForeignKey("dbo.Offers", "LocationImageId", "dbo.ImageModels");
            DropForeignKey("dbo.Offers", "HotelImageId", "dbo.ImageModels");
            AlterColumn("dbo.Offers", "LocationImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Offers", "HotelImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Offers", "HotelName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Offers", "NotIncludedInOffer");
            DropColumn("dbo.Offers", "Observations");
            DropColumn("dbo.Offers", "MealDetails");
            DropColumn("dbo.Offers", "RoomDetails");
            DropColumn("dbo.Offers", "TransferDetails");
            DropColumn("dbo.Offers", "TransportDetails");
            DropColumn("dbo.Offers", "HotelDescription");
            DropColumn("dbo.Offers", "HotelWebSite");
            DropColumn("dbo.Offers", "HotelStars");
            DropColumn("dbo.Offers", "PriceFrom");
            DropColumn("dbo.Offers", "EndDate");
            DropColumn("dbo.Offers", "FromDate");
            DropColumn("dbo.Offers", "Country");
            DropColumn("dbo.Offers", "Location");
            CreateIndex("dbo.Offers", "LocationImageId");
            CreateIndex("dbo.Offers", "HotelImageId");
            AddForeignKey("dbo.Offers", "LocationImageId", "dbo.ImageModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Offers", "HotelImageId", "dbo.ImageModels", "Id", cascadeDelete: true);
        }
    }
}
