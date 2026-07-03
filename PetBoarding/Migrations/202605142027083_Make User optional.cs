namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUseroptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactFormModels", "User_UserId", "dbo.UserModels");
            DropIndex("dbo.ContactFormModels", new[] { "User_UserId" });
            AlterColumn("dbo.ContactFormModels", "User_UserId", c => c.Guid());
            CreateIndex("dbo.ContactFormModels", "User_UserId");
            AddForeignKey("dbo.ContactFormModels", "User_UserId", "dbo.UserModels", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactFormModels", "User_UserId", "dbo.UserModels");
            DropIndex("dbo.ContactFormModels", new[] { "User_UserId" });
            AlterColumn("dbo.ContactFormModels", "User_UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ContactFormModels", "User_UserId");
            AddForeignKey("dbo.ContactFormModels", "User_UserId", "dbo.UserModels", "UserId", cascadeDelete: true);
        }
    }
}
