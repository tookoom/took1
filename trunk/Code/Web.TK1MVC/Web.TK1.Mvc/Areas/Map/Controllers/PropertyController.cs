using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Utility;
using Web.TK1.Mvc.Areas.Map.Models;
using Web.TK1.Mvc.Controllers;

namespace Web.TK1.Mvc.Areas.Map.Controllers
{
    public class PropertyController : BaseMvcController
    {
        //
        // GET: /Map/Property/

        public ActionResult Index()
        {
            return View();
        }

        // Map/Property/Find
        public ActionResult Find(string query)
        {
            //Resources.
            return View();
        }

        //
        // AJAX: Map/Property/SetCulture
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SetCulture(string cultureToSet)
        {
            var reload = true;
            //// Validate input
            //cultureToSet = CultureHelper.GetImplementedCulture(cultureToSet);
            //// Save culture in a cookie
            //HttpCookie cookie = Request.Cookies["_culture"];
            //if (cookie != null)
            //{
            //    if (cookie.Value == cultureToSet)
            //        reload = false;
            //    cookie.Value = cultureToSet;   // update cookie value
            //}
            //else
            //{
            //    cookie = new HttpCookie("_culture");
            //    cookie.Value = cultureToSet;
            //    cookie.Expires = DateTime.Now.AddYears(1);
            //}
            //Response.Cookies.Add(cookie);
            return Json(reload, JsonRequestBehavior.AllowGet);
        }

    }
}
