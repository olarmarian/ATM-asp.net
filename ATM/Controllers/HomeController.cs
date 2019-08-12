using ATM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET /home/index
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;

            return View();
        }

        //GET /home/about
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Having trouble? Send us a message. ";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            ViewBag.Message = "Thanks, we got your message! ";
            return PartialView("_ContactThanks");
        }


        public ActionResult Serial(string letterCase)
        {
            var serial = "CEVA FAIN";
            if(letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            return RedirectToAction("Index");
        }
    }
}