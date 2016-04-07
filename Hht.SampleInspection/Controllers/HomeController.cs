using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hht.SampleInspection.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StandardWork()
        {
            ViewBag.Message = "Click on the links below to open the instructions.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page is still under construction.";

            return View();
        }
    }
}