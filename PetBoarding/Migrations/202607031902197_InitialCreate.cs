namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalTypeModels",
                c => new
                    {
                        AnimalTypeId = c.Guid(nullable: false),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AnimalTypeId);
            
            CreateTable(
                "dbo.PetModels",
                c => new
                    {
                        PetId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Breed = c.String(),
                        Age = c.Int(nullable: false),
                        SpecialInstructions = c.String(maxLength: 1000),
                        AnimalType_AnimalTypeId = c.Guid(nullable: false),
                        EmergencyContact_EmergencyContactId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.AnimalTypeModels", t => t.AnimalType_AnimalTypeId, cascadeDelete: true)
                .ForeignKey("dbo.EmergencyContactModels", t => t.EmergencyContact_EmergencyContactId, cascadeDelete: true)
                .Index(t => t.AnimalType_AnimalTypeId)
                .Index(t => t.EmergencyContact_EmergencyContactId);
            
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
                "dbo.PetOwnerModels",
                c => new
                    {
                        PetOwnerId = c.Guid(nullable: false),
                        Pet_PetId = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PetOwnerId)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Pet_PetId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BookingModels",
                c => new
                    {
                        BookingId = c.Guid(nullable: false),
                        ScheduledCheckIn = c.DateTime(nullable: false),
                        ActualCheckIn = c.DateTime(),
                        ScheduledCheckOut = c.DateTime(nullable: false),
                        ActualCheckOut = c.DateTime(),
                        Status = c.String(),
                        CheckedInBy_EmployeeId = c.Guid(),
                        CheckedOutBy_EmployeeId = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.EmployeeModels", t => t.CheckedInBy_EmployeeId)
                .ForeignKey("dbo.EmployeeModels", t => t.CheckedOutBy_EmployeeId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CheckedInBy_EmployeeId)
                .Index(t => t.CheckedOutBy_EmployeeId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        EmployeeId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        AdminStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ContactFormModels",
                c => new
                    {
                        ContactFormId = c.Guid(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        Responded = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactFormId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PetBookingModels",
                c => new
                    {
                        PetBookingId = c.Guid(nullable: false),
                        Booking_BookingId = c.Guid(nullable: false),
                        Pet_PetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetBookingId)
                .ForeignKey("dbo.BookingModels", t => t.Booking_BookingId, cascadeDelete: true)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetId, cascadeDelete: true)
                .Index(t => t.Booking_BookingId)
                .Index(t => t.Pet_PetId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PetBookingModels", "Pet_PetId", "dbo.PetModels");
            DropForeignKey("dbo.PetBookingModels", "Booking_BookingId", "dbo.BookingModels");
            DropForeignKey("dbo.ContactFormModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookingModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookingModels", "CheckedOutBy_EmployeeId", "dbo.EmployeeModels");
            DropForeignKey("dbo.BookingModels", "CheckedInBy_EmployeeId", "dbo.EmployeeModels");
            DropForeignKey("dbo.PetOwnerModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PetOwnerModels", "Pet_PetId", "dbo.PetModels");
            DropForeignKey("dbo.PetModels", "EmergencyContact_EmergencyContactId", "dbo.EmergencyContactModels");
            DropForeignKey("dbo.PetModels", "AnimalType_AnimalTypeId", "dbo.AnimalTypeModels");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PetBookingModels", new[] { "Pet_PetId" });
            DropIndex("dbo.PetBookingModels", new[] { "Booking_BookingId" });
            DropIndex("dbo.ContactFormModels", new[] { "User_Id" });
            DropIndex("dbo.BookingModels", new[] { "User_Id" });
            DropIndex("dbo.BookingModels", new[] { "CheckedOutBy_EmployeeId" });
            DropIndex("dbo.BookingModels", new[] { "CheckedInBy_EmployeeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PetOwnerModels", new[] { "User_Id" });
            DropIndex("dbo.PetOwnerModels", new[] { "Pet_PetId" });
            DropIndex("dbo.PetModels", new[] { "EmergencyContact_EmergencyContactId" });
            DropIndex("dbo.PetModels", new[] { "AnimalType_AnimalTypeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PetBookingModels");
            DropTable("dbo.ContactFormModels");
            DropTable("dbo.EmployeeModels");
            DropTable("dbo.BookingModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PetOwnerModels");
            DropTable("dbo.EmergencyContactModels");
            DropTable("dbo.PetModels");
            DropTable("dbo.AnimalTypeModels");
        }
    }
}
