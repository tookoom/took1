using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Broker.Controller;

namespace Web.TK1.Mvc.Controllers
{
    public class BrokerController : Controller
    {
        //
        // GET: /Broker/

        public ActionResult Index(string id)
        {
            var adController = new PropertyAdController(id);
            var featureAds = adController.GetFeaturedPropertyAds(PropertyAdTypes.Sell, 5);
            return View(featureAds);
        }

        public ActionResult Imovel(string id, PropertyAdTypes adType, int adCode)
        {
            var adController = new PropertyAdController(id);
            var ad = adController.GetPropertyAdView(adType, adCode);
            return View(ad);
        }
    }
}
