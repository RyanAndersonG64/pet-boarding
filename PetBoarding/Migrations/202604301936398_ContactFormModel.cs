namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactFormModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactFormModels",
                c => new
                    {
                        ContactFormId = c.Guid(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        Responded = c.Boolean(nullable: false),
                        User_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ContactFormId)
                .ForeignKey("dbo.UserModels", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        EmployeeId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        AdminStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactFormModels", "User_UserId", "dbo.UserModels");
            DropIndex("dbo.ContactFormModels", new[] { "User_UserId" });
            DropTable("dbo.EmployeeModels");
            DropTable("dbo.ContactFormModels");
        }
    }
}
