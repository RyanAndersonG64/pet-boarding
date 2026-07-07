using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetBoarding.Models;

namespace PetBoarding.Controllers
{
    public class AdminController : Controller
    {

        //---------------------
        //  Routes and Views
        //---------------------
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ManagePets()
        {
            return View();
        }

        [Authorize]
        public ActionResult ManageBookings()
        {
            return View();
        }

        [Authorize]
        public ActionResult ManageContactForms()
        {
            return View();
        }
        //---------------------
        //  CRUD
        //---------------------

        // Creation logic

        public ActionResult AddEmployee()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            EmployeeModel newEmployee = new EmployeeModel();

            newEmployee.Name = Request.Form["Name"];
            newEmployee.AdminStatus = false;

            context.Employees.Add(newEmployee);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the employee: " + ex.Message;
                return View("Index");
            }
            return View();
        }

        // Read logic
        public ActionResult ViewPet(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            PetModel pet = dbContext.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return Content($"{pet.Name} :/n Age - {pet.Age} /n Breed - {pet.Breed} /n Special Instructions - {pet.SpecialInstructions} /n Emergency Contact - {pet.EmergencyContact?.Name}");
        }

        // Update logic
        public ActionResult UpdatePet(Guid id, PetModel updatedPet)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            PetModel pet = dbContext.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            // Update pet properties here
            pet.Name = updatedPet.Name;
            pet.Age = updatedPet.Age;
            pet.Breed = updatedPet.Breed;
            pet.SpecialInstructions = updatedPet.SpecialInstructions;
            pet.EmergencyContact = updatedPet.EmergencyContact;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while updating the pet: " + ex.Message;
                return View("ManagePets");
            }
            return View("ManagePets");
        }






        // Delete logic

        public ActionResult DeletePetOwner(Guid id, PetModel pet)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            PetOwnerModel petOwner = dbContext.PetOwners.FirstOrDefault(po => po.PetOwnerId == id);
            if (petOwner == null)
            {
                return HttpNotFound();
            }
            dbContext.PetOwners.Remove(petOwner);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the pet owner: " + ex.Message;
                return View("ManagePets");
            }
            return RedirectToAction("ManagePets");
        }

        public ActionResult DeletePetBooking(Guid id, PetModel pet)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            PetBookingModel petBooking = dbContext.PetBookings.FirstOrDefault(pb => pb.PetBookingId == id);
            if (petBooking == null)
            {
                return HttpNotFound();
            }
            dbContext.PetBookings.Remove(petBooking);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the pet booking: " + ex.Message;
                return View("ManageBookings");
            }
            return RedirectToAction("ManageBookings");
        }

        public ActionResult DeletePet(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            PetModel pet = dbContext.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            // DeletePetOwner for all PetOwners associated with this pet
            var petOwners = dbContext.PetOwners.Where(po => po.Pet.PetId == id).ToList();
            foreach (var petOwner in petOwners)
            {
                DeletePetOwner(petOwner.PetOwnerId, pet);
            }

            // DeletePetBooking for all PetBookings associated with this pet
            var petBookings = dbContext.PetBookings.Where(pb => pb.Pet.PetId == id).ToList();
            foreach (var petBooking in petBookings)
            {
                DeletePetBooking(petBooking.PetBookingId, pet);
            }

            dbContext.Pets.Remove(pet);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the pet: " + ex.Message;
                return View("ManagePets");
            }

            return RedirectToAction("ManagePets");
        }

        public ActionResult DeleteBooking(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            BookingModel booking = dbContext.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            // DeletePetBooking for all PetBookings associated with this booking
            var petBookings = dbContext.PetBookings.Where(pb => pb.Booking.BookingId == id).ToList();
            foreach (var petBooking in petBookings)
            {
                DeletePetBooking(petBooking.PetBookingId, petBooking.Pet);
            }

            dbContext.Bookings.Remove(booking);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the booking: " + ex.Message;
                return View("ManageBookings");
            }
            return RedirectToAction("ManageBookings");
        }

        public ActionResult DeleteEmergencyContact(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            EmergencyContactModel emergencyContact = dbContext.EmergencyContacts.FirstOrDefault(ec => ec.EmergencyContactId == id);
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            // Remove the emergency contact being deleted from all pets that have it
            var pets = dbContext.Pets.Where(p => p.EmergencyContact.EmergencyContactId == id).ToList();
            foreach (var pet in pets)
            {
                UpdatePet(pet.PetId, new PetModel
                {
                    Name = pet.Name,
                    Age = pet.Age,
                    Breed = pet.Breed,
                    SpecialInstructions = pet.SpecialInstructions,
                    EmergencyContact = null
                });
            }

            dbContext.EmergencyContacts.Remove(emergencyContact);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the emergency contact: " + ex.Message;
                return View("ManageEmergencyContacts");
            }

            return RedirectToAction("ManageEmergencyContacts");
        }

        public ActionResult DeleteContactForm(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            ContactFormModel contactForm = dbContext.ContactForms.FirstOrDefault(cf => cf.ContactFormId == id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            dbContext.ContactForms.Remove(contactForm);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the contact form: " + ex.Message;
                return View("ManageContactForms");
            }
            return RedirectToAction("ManageContactForms");
        }

        public ActionResult DeleteEmployee(Guid id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            EmployeeModel employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            dbContext.Employees.Remove(employee);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while deleting the employee: " + ex.Message;
                return View("ManageEmployees");
            }
            return RedirectToAction("ManageEmployees");
        }
    }
}