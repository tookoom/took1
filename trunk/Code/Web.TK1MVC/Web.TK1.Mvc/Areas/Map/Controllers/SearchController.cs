using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK1.Bizz.Broker.Presentation;
using TK1.Data;
using TK1.Data.Bizz.Broker.Controller;
using TK1.Data.Bizz.Broker.Model;
using TK1.Data.Bizz.Model;
using Web.TK1.Mvc.Areas.Map.Models;
using Web.TK1.Mvc.Controllers;

namespace Web.TK1.Mvc.Areas.Map.Controllers
{
    public class SearchController : BaseMvcController
    {
        private static string customerCode = "citymapper.ca";

        //
        // GET: /Map/Search/

        public ActionResult Index()
        {
            return View();
        }

        //
        // AJAX: /Search/SearchByCity
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SearchByCity(float latMin, float latMax, float lonMin, float lonMax)
        {
            var adController = new PropertyAdController(customerCode) { LoadCategory = false, LoadDetails = true, LoadPics = false };
            var searchParams = new List<SearchAttribute>();
            searchParams.Add(new SearchAttribute()
            {
                Attribute = PropertyAdSearchAttributes.CityLatitude,
                Operator = SearchOperators.BETWEEN,
                MinValue = latMin,
                MaxValue = latMax
            });
            searchParams.Add(new SearchAttribute()
            {
                Attribute = PropertyAdSearchAttributes.CityLongitude,
                Operator = SearchOperators.BETWEEN,
                MinValue = lonMin,
                MaxValue = lonMax
            });
            var query = adController.SearchQuery(searchParams);
            var items = (from o in query
                         group o by new { o.CityLatitude, o.CityLongitude }
                           into grp
                               select new JsonMapItem()
                               {
                                   Description = SqlFunctions.StringConvert((double)grp.Count()).Trim() + Resources.InfoBox_Text_GroupAd,
                                   Latitude = grp.Key.CityLatitude.Value,
                                   Longitude = grp.Key.CityLongitude.Value,
                                   Source = "",
                                   Title = grp.Max(o => o.CityName),
                                   Url = "",
                                   Value = ""
                               }).ToList();

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        //
        // AJAX: /Search/SearchByLocation
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SearchByLocation(float latMin, float latMax, float lonMin, float lonMax)
        {
            var items = new List<JsonMapItem>();
            var adController = new PropertyAdController(customerCode) { LoadCategory = false, LoadDetails = true, LoadPics = false };
            var searchParams = new List<SearchAttribute>();
            searchParams.Add(new SearchAttribute()
            {
                Attribute = PropertyAdSearchAttributes.PropertyLatitude,
                Operator = SearchOperators.BETWEEN,
                MinValue = latMin,
                MaxValue = latMax
            });
            searchParams.Add(new SearchAttribute()
            {
                Attribute = PropertyAdSearchAttributes.PropertyLongitude,
                Operator = SearchOperators.BETWEEN,
                MinValue = lonMin,
                MaxValue = lonMax
            });
            foreach (var item in adController.Search(searchParams))
            {
                items.Add(new JsonMapItem()
                {
                    Description = item.Title,
                    Latitude = item.Location.Latitude.Value,
                    Longitude = item.Location.Longitude.Value,
                    Source = item.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_SOURCE).Select(o => o.Value).FirstOrDefault(),
                    Title = "Kijiji",
                    Url = item.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_URL).Select(o => o.Value).FirstOrDefault(),
                    Value = item.Value.ToString("c")
                });
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        //
        // AJAX: /Search/SearchByLocation
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SearchByLocationGroup(float latMin, float latMax, float lonMin, float lonMax)
        {
            var adController = new PropertyAdController(customerCode) { LoadCategory = false, LoadDetails = true, LoadPics = false };
            var result = new List<JsonMapItem>();
            var slice = 8;
            var latSlice = (latMax - latMin) / slice;
            var lonSlice = (lonMax - lonMin) / slice;

            for (int i = 0; i < slice; i++)
            {
                for (int j = 0; j < slice; j++)
                {
                    var latSliceMin = latMin + (i * latSlice);
                    var latSliceMax = latMin + ((i + 1) * latSlice);
                    var lonSliceMin = lonMin + (j * lonSlice);
                    var lonSliceMax = lonMin + ((j + 1) * lonSlice);
                    var searchParams = new List<SearchAttribute>();
                    searchParams.Add(new SearchAttribute()
                    {
                        Attribute = PropertyAdSearchAttributes.PropertyLatitude,
                        Operator = SearchOperators.BETWEEN,
                        MinValue = latSliceMin,
                        MaxValue = latSliceMax
                    });
                    searchParams.Add(new SearchAttribute()
                    {
                        Attribute = PropertyAdSearchAttributes.PropertyLongitude,
                        Operator = SearchOperators.BETWEEN,
                        MinValue = lonSliceMin,
                        MaxValue = lonSliceMax
                    });

                    var count = adController.SearchQuery(searchParams).Count();
                    if (count > 0)
                    {
                        result.Add(new JsonMapItem()
                                     {
                                         Description = count.ToString() + Resources.InfoBox_Text_GroupAd,
                                         Latitude = latSliceMin + (latSlice / 2),
                                         Longitude = lonSliceMin + (lonSlice / 2),
                                         Source = "",
                                         Title = "",
                                         Url = "",
                                         Value = ""
                                     });
                    }
                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            base.IgnoreCultureChange = true;
            return base.BeginExecuteCore(callback, state);
        }


        //public JsonResult SearchByLocationGroup_OLD(float latMin, float latMax, float lonMin, float lonMax)
        //{
        //    var adController = new PropertyAdController(customerCode) { LoadCategory = false, LoadDetails = true, LoadPics = false };
        //    var searchParams = new List<SearchAttribute>();
        //    searchParams.Add(new SearchAttribute()
        //    {
        //        Attribute = PropertyAdSearchAttributes.DistrictLatitude,
        //        Operator = SearchOperators.BETWEEN,
        //        MinValue = latMin,
        //        MaxValue = latMax
        //    });
        //    searchParams.Add(new SearchAttribute()
        //    {
        //        Attribute = PropertyAdSearchAttributes.DistrictLongitude,
        //        Operator = SearchOperators.BETWEEN,
        //        MinValue = lonMin,
        //        MaxValue = lonMax
        //    });
        //    var query = adController.SearchQuery(searchParams);
        //    var items = (from o in query
        //                 group o by new { o.DistrictLatitude, o.DistrictLongitude }
        //                     into grp
        //                     select new JsonMapItem()
        //                     {
        //                         Description = SqlFunctions.StringConvert((double)grp.Count()).Trim() + Resources.InfoBox_Text_GroupAd,
        //                         Latitude = grp.Key.DistrictLatitude.Value,
        //                         Longitude = grp.Key.DistrictLongitude.Value,
        //                         Source = "",
        //                         Title = grp.Max(o => o.DistrictName),
        //                         Url = "",
        //                         Value = ""
        //                     }).ToList();

        //    return Json(items, JsonRequestBehavior.AllowGet);
        //}
    }
}
