namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddNameAndPhoneNumberToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
