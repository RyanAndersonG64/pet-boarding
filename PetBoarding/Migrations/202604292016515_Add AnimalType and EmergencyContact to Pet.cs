namespace WebAppTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnimalTypeandEmergencyContacttoPet : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PetModels", name: "AnimalTypeModel_AnimalTypeId", newName: "AnimalType_AnimalTypeId");
            RenameColumn(table: "dbo.PetModels", name: "EmergencyContactModel_EmergencyContactId", newName: "EmergencyContact_EmergencyContactId");
            RenameIndex(table: "dbo.PetModels", name: "IX_AnimalTypeModel_AnimalTypeId", newName: "IX_AnimalType_AnimalTypeId");
            RenameIndex(table: "dbo.PetModels", name: "IX_EmergencyContactModel_EmergencyContactId", newName: "IX_EmergencyContact_EmergencyContactId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PetModels", name: "IX_EmergencyContact_EmergencyContactId", newName: "IX_EmergencyContactModel_EmergencyContactId");
            RenameIndex(table: "dbo.PetModels", name: "IX_AnimalType_AnimalTypeId", newName: "IX_AnimalTypeModel_AnimalTypeId");
            RenameColumn(table: "dbo.PetModels", name: "EmergencyContact_EmergencyContactId", newName: "EmergencyContactModel_EmergencyContactId");
            RenameColumn(table: "dbo.PetModels", name: "AnimalType_AnimalTypeId", newName: "AnimalTypeModel_AnimalTypeId");
        }
    }
}
