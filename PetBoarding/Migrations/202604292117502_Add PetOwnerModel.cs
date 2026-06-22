namespace WebAppTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPetOwnerModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PetModels", "AnimalType_AnimalTypeId", "dbo.AnimalTypeModels");
            DropForeignKey("dbo.PetModels", "EmergencyContact_EmergencyContactId", "dbo.EmergencyContactModels");
            DropIndex("dbo.PetModels", new[] { "AnimalType_AnimalTypeId" });
            DropIndex("dbo.PetModels", new[] { "EmergencyContact_EmergencyContactId" });
            AlterColumn("dbo.PetModels", "AnimalType_AnimalTypeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PetModels", "EmergencyContact_EmergencyContactId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PetModels", "AnimalType_AnimalTypeId");
            CreateIndex("dbo.PetModels", "EmergencyContact_EmergencyContactId");
            AddForeignKey("dbo.PetModels", "AnimalType_AnimalTypeId", "dbo.AnimalTypeModels", "AnimalTypeId", cascadeDelete: true);
            AddForeignKey("dbo.PetModels", "EmergencyContact_EmergencyContactId", "dbo.EmergencyContactModels", "EmergencyContactId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetModels", "EmergencyContact_EmergencyContactId", "dbo.EmergencyContactModels");
            DropForeignKey("dbo.PetModels", "AnimalType_AnimalTypeId", "dbo.AnimalTypeModels");
            DropIndex("dbo.PetModels", new[] { "EmergencyContact_EmergencyContactId" });
            DropIndex("dbo.PetModels", new[] { "AnimalType_AnimalTypeId" });
            AlterColumn("dbo.PetModels", "EmergencyContact_EmergencyContactId", c => c.Guid());
            AlterColumn("dbo.PetModels", "AnimalType_AnimalTypeId", c => c.Guid());
            CreateIndex("dbo.PetModels", "EmergencyContact_EmergencyContactId");
            CreateIndex("dbo.PetModels", "AnimalType_AnimalTypeId");
            AddForeignKey("dbo.PetModels", "EmergencyContact_EmergencyContactId", "dbo.EmergencyContactModels", "EmergencyContactId");
            AddForeignKey("dbo.PetModels", "AnimalType_AnimalTypeId", "dbo.AnimalTypeModels", "AnimalTypeId");
        }
    }
}
