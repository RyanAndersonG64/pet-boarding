using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTemplate.Models;

namespace WebAppTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hours()
        {
            ViewBag.Message = "Hours";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult AddContactForm()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            ContactFormModel newContactForm = new ContactFormModel();
            newContactForm.User = null;
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
                return View("Index");
            }
            return RedirectToAction("Index");
        }
    }
}