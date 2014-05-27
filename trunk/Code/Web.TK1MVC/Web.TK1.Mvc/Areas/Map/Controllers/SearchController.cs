using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.TK1.Mvc.Areas.Map.Models;

namespace Web.TK1.Mvc.Areas.Map.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Map/Search/

        public ActionResult Index()
        {
            return View();
        }

        //
        // AJAX: /Search/SearchByLocation
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SearchByLocation(float longitude, float latitude)
        {
            var items = new List<JsonMapItem>();
            items.Add(new JsonMapItem() { Description = "Test", Latitude = 46.783445, Longitude = -71.254326, Title = "Title Test" });

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
