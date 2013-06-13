using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Bizz.Broker.Presentation;
using TK1.Bizz.Broker.Presentation.Culture;
using TK1.Data.Bizz.Broker.Controller;
using Web.TK1.Mvc.Areas.Bizz.Models;
using MvcPaging;

namespace Web.TK1.Mvc.Areas.Bizz.Controllers
{
    public class BrokerController : Controller
    {
        //
        // GET: /Bizz/Broker/
        public ActionResult Index(string customerCode)
        {
            ViewBag.CustomerCode = customerCode;
            var adController = new PropertyAdController(customerCode);

            IEnumerable<SelectListItem> adTypes = adController.GetAdTypes().Select(o => new SelectListItem
            {
                Value = o.ToString(),
                Text = PropertyTranslations.GetAdTypeDisplayName(o,"pt-BR")
            });
            ViewBag.AdType = adTypes;

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
        public ActionResult Contato(string customerCode)
        {
            return View(new PropertyAdView() { CustomerCode = customerCode ?? "NULL" });
        }
        public ActionResult Imovel(string customerCode, PropertyAdTypes adType, int adCode)
        {
            var adController = new PropertyAdController(customerCode);

            ViewBag.Details = adController.GetPropertyDetailViews(adType, adCode);
            ViewBag.Pics = adController.GetPropertyPicViews(adType, adCode);

            var ad = adController.GetPropertyAdView(adType, adCode);
            return View(ad);
        }
        public ActionResult Lancamento(string customerCode, int adCode)
        {
            var adController = new PropertyAdController(customerCode);
            var ad = adController.GetPropertyReleaseAdView(adCode);
            return View(ad);
        }
        public ActionResult Pesquisa(string customerCode, string adType, int? page)
        {
            int pageIndex = 0;
            if (page.HasValue)
                pageIndex = page.Value > 0 ? page.Value - 1 : 0;
            int pageSize = getDefaultPageSize(customerCode);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            PropertyAdTypes propertyAdType = PropertyTranslations.GetAdTypeFromDisplayName(adType, getDefaultAdType(customerCode));
            var adController = new PropertyAdController(customerCode);

            var searchParameters = getSearchParameters(customerCode, propertyAdType);
            loadSearchParameters(customerCode, searchParameters);

            var searchResults = adController.SearchPropertyAds(searchParameters);
            int count = searchResults.Count();
            ViewBag.SearchResults = searchResults;
            ViewBag.SearchResultsCount = count;

            //ViewBag.PagedSearchResults = searchResults.ToPagedList(pageIndex, pageSize, count);
            return View();
        }
        public ActionResult Sobre(string customerCode)
        {
            return View(new PropertyAdView() { CustomerCode = customerCode ?? "NULL" });
        }

        #region AJAX
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Cities(string customerCode, PropertyAdTypes adType)
        {
            var adController = new PropertyAdController(customerCode);
            var defaultCity = getDefaultCity(customerCode);
            IEnumerable<SelectListItem> cities = adController.GetCities(adType).Select(o => new SelectListItem
            {
                Value = o,
                Text = o,
                Selected = o == defaultCity
            });
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        #endregion


        private PropertyAdTypes getDefaultAdType(string customerCode)
        {
            return PropertyAdTypes.Sell;
        }
        private string getDefaultCity(string customerCode)
        {
            return "Porto Alegre";
        }
        private int getDefaultPageSize(string customerCode)
        {
            return 200;
        }
        private PropertyAdSearchParameters getSearchParameters(string customerCode, PropertyAdTypes adType)
        {
            var result = Session[BrokerSessionKeys.SearchParameters] as PropertyAdSearchParameters;
            if (result == null)
            {
                result = new PropertyAdSearchParameters()
                {
                    CustomerCode = customerCode,
                    AdType = adType,

                    CityName = getDefaultCity(customerCode)
                };
                result.Districts.Add("*");
                Session[BrokerSessionKeys.SearchParameters] = result;
            }
            return result;
        }
        private void loadSearchParameters(string customerCode, PropertyAdSearchParameters parameters)
        {
            var adController = new PropertyAdController(customerCode);

            IEnumerable<SelectListItem> adTypes = adController.GetAdTypes().Select(o => new SelectListItem
            {
                Value = o.ToString(),
                Text = PropertyTranslations.GetAdTypeDisplayName(o, "pt-BR"),
                Selected = o == parameters.AdType
            });
            ViewBag.AdType = adTypes;

            IEnumerable<SelectListItem> cities = adController.GetCities().Select(o => new SelectListItem
            {
                Value = o,
                Text = o,
                Selected = o == parameters.CityName
            });
            ViewBag.City = cities;

            IEnumerable<SelectListItem> propertyTypes = adController.GetPropertyTypes().Select(o => new SelectListItem
            {
                Value = o,
                Text = o,
                Selected = o == parameters.PropertyType
            });
            ViewBag.PropertyType = propertyTypes;

            IEnumerable<SelectListItem> districts = adController.GetDistricts().Select(o => new SelectListItem
            {
                Value = o,
                Text = o,
                Selected = parameters.Districts.Contains(o)
            });
            ViewBag.District = districts;
        }

    }
}
