namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetAnimalTypeEmergencyContact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalTypeModels",
                c => new
                    {
                        AnimalTypeId = c.Guid(nullable: false),
                        type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AnimalTypeId);
            
            CreateTable(
                "dbo.EmergencyContactModels",
                c => new
                    {
                        EmergencyContactId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.EmergencyContactId);
            
            CreateTable(
                "dbo.PetModels",
                c => new
                    {
                        PetId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Breed = c.String(),
                        Age = c.Int(nullable: false),
                        SpecialInstructions = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.PetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PetModels");
            DropTable("dbo.EmergencyContactModels");
            DropTable("dbo.AnimalTypeModels");
        }
    }
}
