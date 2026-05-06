using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTemplate.Models;

namespace WebAppTemplate.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManagePets()
        {
            return View();
        }

        public ActionResult ManageBookings()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        // Creation logic
        public ActionResult AddPetOwner(PetModel pet)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            PetOwnerModel newPetOwner = new PetOwnerModel();
            newPetOwner.User = new ApplicationDbContext().Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            newPetOwner.Pet = pet;

            context.PetOwners.Add(newPetOwner);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the pet owner: " + ex.Message;
                return View("ManagePets");
            }
            return RedirectToAction("ManagePets");
        }

        public ActionResult AddPetBooking (PetModel pet, BookingModel booking)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            PetBookingModel newPetBooking = new PetBookingModel();
            newPetBooking.Pet = pet;
            newPetBooking.Booking = booking;

            context.PetBookings.Add(newPetBooking);
            try 
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the pet booking: " + ex.Message;
                return View("ManageBookings");
            }
            return RedirectToAction("ManageBookings");
        }

        public ActionResult AddEmergencyContact()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            EmergencyContactModel newContact = new EmergencyContactModel();
            newContact.Name = Request.Form["Name"];
            newContact.PhoneNumber = Request.Form["PhoneNumber"];
            dbContext.EmergencyContacts.Add(newContact);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the emergency contact: " + ex.Message;
                return View("ManagePets");
            }
            return RedirectToAction("ManagePets");
        }

        public ActionResult AddPet()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            PetModel newPet = new PetModel();
            newPet.Name = Request.Form["Name"];
            newPet.Breed = Request.Form["Breed"];
            newPet.AnimalType = dbContext.AnimalTypes.FirstOrDefault(a => a.Type == Request.Form["AnimalType"]);
            newPet.Age = int.Parse(Request.Form["Age"]);
            newPet.SpecialInstructions = Request.Form["SpecialInstructions"];
            newPet.EmergencyContact = dbContext.EmergencyContacts.FirstOrDefault(e => e.EmergencyContactId == Guid.Parse(Request.Form["EmergencyContactId"]));

            dbContext.Pets.Add(newPet);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the pet: " + ex.Message;
                return View("ManagePets");
            }
            AddPetOwner(newPet);
            return RedirectToAction("ManagePets");
        }

        public ActionResult AddBooking ()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            BookingModel newBooking = new BookingModel();
            newBooking.User = dbContext.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            newBooking.ScheduledCheckIn = DateTime.Parse(Request.Form["ScheduledCheckIn"]);
            newBooking.ActualCheckIn = null;
            newBooking.CheckedInBy = null;
            newBooking.ScheduledCheckOut = DateTime.Parse(Request.Form["ScheduledCheckOut"]);
            newBooking.ActualCheckOut = null;
            newBooking.CheckedOutBy = null;
            newBooking.Status = "Upcoming";

            dbContext.Bookings.Add(newBooking);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while adding the booking: " + ex.Message;
                return View("ManageBookings");

            }
            AddPetBooking(dbContext.Pets.FirstOrDefault(p => p.PetId == Guid.Parse(Request.Form["PetId"])), newBooking);
            return RedirectToAction("ManageBookings");
        }

        public ActionResult AddContactForm()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            ContactFormModel newContactForm = new ContactFormModel();
            newContactForm.User = dbContext.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            newContactForm.Subject = Request.Form["Subject"];
            newContactForm.Body = Request.Form["Body"];
            newContactForm.Responded = false;

            dbContext.ContactForms.Add(newContactForm);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message, etc.)
                // For simplicity, we will just return the error message in the view.
                ViewBag.ErrorMessage = "An error occurred while submitting the contact form: " + ex.Message;
                return View("ContactUs");
            }
            return RedirectToAction("ContactUs");
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
            return Content($"{ pet.Name} :/n Age - { pet.Age} /n Breed - { pet.Breed} /n Special Instructions - { pet.SpecialInstructions} /n Emergency Contact - { pet.EmergencyContact?.Name}");
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
                return View("ContactUs");
            }
            return RedirectToAction("ContactUs");
        }
    }
}