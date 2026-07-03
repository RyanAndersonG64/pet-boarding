namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Makeactualdatesnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookingModels", "ActualCheckIn", c => c.DateTime());
            AlterColumn("dbo.BookingModels", "ActualCheckOut", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookingModels", "ActualCheckOut", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BookingModels", "ActualCheckIn", c => c.DateTime(nullable: false));
        }
    }
}
