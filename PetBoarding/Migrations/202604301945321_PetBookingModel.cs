namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetBookingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetBookingModels",
                c => new
                    {
                        PetBookingId = c.Guid(nullable: false),
                        Booking_BookingId = c.Guid(nullable: false),
                        Pet_PetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetBookingId)
                .ForeignKey("dbo.BookingModels", t => t.Booking_BookingId, cascadeDelete: true)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetId, cascadeDelete: true)
                .Index(t => t.Booking_BookingId)
                .Index(t => t.Pet_PetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetBookingModels", "Pet_PetId", "dbo.PetModels");
            DropForeignKey("dbo.PetBookingModels", "Booking_BookingId", "dbo.BookingModels");
            DropIndex("dbo.PetBookingModels", new[] { "Pet_PetId" });
            DropIndex("dbo.PetBookingModels", new[] { "Booking_BookingId" });
            DropTable("dbo.PetBookingModels");
        }
    }
}
