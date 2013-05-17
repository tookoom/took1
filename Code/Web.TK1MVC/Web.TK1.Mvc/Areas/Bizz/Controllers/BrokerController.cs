using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Bizz.Broker.Presentation;
using TK1.Bizz.Broker.Presentation.Culture;
using TK1.Data.Bizz.Broker.Controller;

namespace Web.TK1.Mvc.Areas.Bizz.Controllers
{
    public class BrokerController : Controller
    {
        //
        // GET: /Bizz/Broker/

        public ActionResult Index(string customerCode)
        {
            var adController = new PropertyAdController(customerCode);
            ViewBag.CustomerCode = customerCode;

            IEnumerable<SelectListItem> adTypes = adController.GetAdTypes().Select(o => new SelectListItem
            {
                Value = o.ToString(),
                Text = PropertyTranslations.GetAdTypeDisplayName(o,"pt-BR")
            });
            ViewBag.AdType = adTypes;

            IEnumerable<SelectListItem> cities = adController.GetCities().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.City = cities;

            IEnumerable<SelectListItem> propertyTypes = adController.GetPropertyTypes().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.PropertyType = propertyTypes;

            IEnumerable<SelectListItem> districts = adController.GetDistricts().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.District = districts;

            ViewBag.SellFeaturedAds = adController.GetFeaturedPropertyAds(PropertyAdTypes.Sell, 4);
            ViewBag.RentFeaturedAds = adController.GetFeaturedPropertyAds(PropertyAdTypes.Rent, 4);
            return View();
        }

        //
        // GET: /Bizz/Broker/Contato/demo

        public ActionResult Contato(string customerCode)
        {
            return View(new PropertyAdView() { CustomerCode = customerCode ?? "NULL" });
        }

        //
        // GET: /Bizz/Broker/Imovel/demo/Sell/1

        public ActionResult Imovel(string customerCode, PropertyAdTypes adType, int adCode)
        {
            var adController = new PropertyAdController(customerCode);

            ViewBag.Pics = adController.GetPropertyPicViews(adType, adCode);

            var ad = adController.GetPropertyAdView(adType, adCode);
            return View(ad);
        }

        //
        // GET: /Bizz/Broker/Imovel/demo/Sell/1

        public ActionResult Lancamento(string customerCode, int adCode)
        {
            var adController = new PropertyAdController(customerCode);
            var ad = adController.GetPropertyReleaseAdView(adCode);
            return View(ad);
        }

        //
        // GET: /Bizz/Broker/Pesquisa/demo

        public ActionResult Pesquisa(string customerCode)
        {
            var adController = new PropertyAdController(customerCode);

            IEnumerable<SelectListItem> adTypes = adController.GetAdTypes().Select(o => new SelectListItem
            {
                Value = o.ToString(),
                Text = PropertyTranslations.GetAdTypeDisplayName(o, "pt-BR")
            });
            ViewBag.AdType = adTypes;

            IEnumerable<SelectListItem> cities = adController.GetCities().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.City = cities;

            IEnumerable<SelectListItem> propertyTypes = adController.GetPropertyTypes().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.PropertyType = propertyTypes;

            IEnumerable<SelectListItem> districts = adController.GetDistricts().Select(o => new SelectListItem
            {
                Value = o,
                Text = o
            });
            ViewBag.District = districts;

            return View(new PropertyAdSearchParameters() { CustomerCode = customerCode ?? "NULL" });
        }

        //
        // GET: /Bizz/Broker/Sobre/demo

        public ActionResult Sobre(string customerCode)
        {
            return View(new PropertyAdView() { CustomerCode = customerCode ?? "NULL" });
        }
    }
}
