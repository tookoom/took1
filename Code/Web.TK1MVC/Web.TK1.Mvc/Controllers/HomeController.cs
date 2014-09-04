using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Utility;

namespace Web.TK1.Mvc.Controllers
{
    public class HomeController : BaseMvcController
    {
        public ActionResult Index()
        {
            var redirect = false;

#if DEBUG
            redirect = true;
#endif

            if (Request.Url.AbsoluteUri.Contains("citymapper"))
                redirect = true;

            if (redirect)
            {
                //var cookieCulture = cookie == null ? null : cookie.Value as string;
                return RedirectToAction("Find", "Property", new { area = "map", query = "Québec" });
            }

            ViewBag.FeaturedText = "Rápido e fácil.";
            ViewBag.Message = "Do jeito que você precisa.";
            ViewBag.Message = Request.Url.AbsoluteUri;

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
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }  
    }
}
