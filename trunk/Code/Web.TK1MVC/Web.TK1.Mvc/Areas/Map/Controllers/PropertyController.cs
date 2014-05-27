using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.TK1.Mvc.Areas.Map.Models;

namespace Web.TK1.Mvc.Areas.Map.Controllers
{
    public class PropertyController : Controller
    {
        //
        // GET: /Map/Property/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Find()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search()//(float longitude, float latitude)
        {
            var items = new List<JsonMapItem>();
            items.Add(new JsonMapItem() { Description = "Test", Latitude = 40, Longitude = -70, Title = "Title Test" });

            return Json(items);
        }
    }
}
