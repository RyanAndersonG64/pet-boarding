using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}