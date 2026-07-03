using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetBoarding.IdentityModels;

namespace PetBoarding.Models
{


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PetModel> Pets { get; set; }
        public DbSet<AnimalTypeModel> AnimalTypes { get; set; }
        public DbSet<EmergencyContactModel> EmergencyContacts { get; set; }
        public DbSet<PetOwnerModel> PetOwners { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<ContactFormModel> ContactForms { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<PetBookingModel> PetBookings { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}