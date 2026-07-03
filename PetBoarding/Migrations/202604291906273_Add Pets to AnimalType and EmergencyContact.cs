namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPetstoAnimalTypeandEmergencyContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetModels", "AnimalTypeModel_AnimalTypeId", c => c.Guid());
            AddColumn("dbo.PetModels", "EmergencyContactModel_EmergencyContactId", c => c.Guid());
            CreateIndex("dbo.PetModels", "AnimalTypeModel_AnimalTypeId");
            CreateIndex("dbo.PetModels", "EmergencyContactModel_EmergencyContactId");
            AddForeignKey("dbo.PetModels", "AnimalTypeModel_AnimalTypeId", "dbo.AnimalTypeModels", "AnimalTypeId");
            AddForeignKey("dbo.PetModels", "EmergencyContactModel_EmergencyContactId", "dbo.EmergencyContactModels", "EmergencyContactId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetModels", "EmergencyContactModel_EmergencyContactId", "dbo.EmergencyContactModels");
            DropForeignKey("dbo.PetModels", "AnimalTypeModel_AnimalTypeId", "dbo.AnimalTypeModels");
            DropIndex("dbo.PetModels", new[] { "EmergencyContactModel_EmergencyContactId" });
            DropIndex("dbo.PetModels", new[] { "AnimalTypeModel_AnimalTypeId" });
            DropColumn("dbo.PetModels", "EmergencyContactModel_EmergencyContactId");
            DropColumn("dbo.PetModels", "AnimalTypeModel_AnimalTypeId");
        }
    }
}
