using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.TK1.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FeaturedText = "Rápido e fácil.";
            ViewBag.Message = "Do jeito que você precisa.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Broker.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
