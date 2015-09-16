using System;
using System.Web.Mvc;
using firstMvcApp.Models;

namespace firstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                // todo: email guest response to the party organizer
                return View("Thanks", guestResponse);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

        public string Index2()
        {
            return "Hello World";
        }

        public ActionResult Index3()
        {
            return View("Index");
        }

    }
}