using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public string Index2()
        {
            return "Hello World";
        }

        public ActionResult Index3()
        {
            return View();
        }
    }
}