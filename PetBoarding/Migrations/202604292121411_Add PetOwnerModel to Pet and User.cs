namespace WebAppTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPetOwnerModeltoPetandUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetOwnerModels",
                c => new
                    {
                        PetOwnerId = c.Guid(nullable: false),
                        Pet_PetId = c.Guid(nullable: false),
                        User_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetOwnerId)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetId, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Pet_PetId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetOwnerModels", "User_UserId", "dbo.UserModels");
            DropForeignKey("dbo.PetOwnerModels", "Pet_PetId", "dbo.PetModels");
            DropIndex("dbo.PetOwnerModels", new[] { "User_UserId" });
            DropIndex("dbo.PetOwnerModels", new[] { "Pet_PetId" });
            DropTable("dbo.PetOwnerModels");
        }
    }
}
