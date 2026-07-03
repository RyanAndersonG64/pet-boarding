namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingModels",
                c => new
                    {
                        BookingId = c.Guid(nullable: false),
                        ScheduledCheckIn = c.DateTime(nullable: false),
                        ActualCheckIn = c.DateTime(nullable: false),
                        ScheduledCheckOut = c.DateTime(nullable: false),
                        ActualCheckOut = c.DateTime(nullable: false),
                        Status = c.String(),
                        CheckedInBy_EmployeeId = c.Guid(),
                        CheckedOutBy_EmployeeId = c.Guid(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.EmployeeModels", t => t.CheckedInBy_EmployeeId)
                .ForeignKey("dbo.EmployeeModels", t => t.CheckedOutBy_EmployeeId)
                .ForeignKey("dbo.UserModels", t => t.User_UserId)
                .Index(t => t.CheckedInBy_EmployeeId)
                .Index(t => t.CheckedOutBy_EmployeeId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingModels", "User_UserId", "dbo.UserModels");
            DropForeignKey("dbo.BookingModels", "CheckedOutBy_EmployeeId", "dbo.EmployeeModels");
            DropForeignKey("dbo.BookingModels", "CheckedInBy_EmployeeId", "dbo.EmployeeModels");
            DropIndex("dbo.BookingModels", new[] { "User_UserId" });
            DropIndex("dbo.BookingModels", new[] { "CheckedOutBy_EmployeeId" });
            DropIndex("dbo.BookingModels", new[] { "CheckedInBy_EmployeeId" });
            DropTable("dbo.BookingModels");
        }
    }
}
