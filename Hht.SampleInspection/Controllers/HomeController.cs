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
        public ActionResult Report()
        {
            ViewBag.Message = "Click on the links below to open a report.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Sample Inspection application support";

            return View();
        }
    }
}