using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TK1.Bizz.Broker.Presentation;
using TK1.Collection;
using TK1.Data.Bizz.Model;
using TK1.Data.Converter;
using TK1.Html;
using TK1.Html.Bot;
using TK1.Html.Bot.Google;
using TK1.Html.Elements;
using TK1.Net;
using TK1.Net.Services;
using TK1.Bizz.Mapper.Model;
using TK1.Web;
using TK1.Utility;

namespace TK1.Bizz.Broker.Bot.Sources
{
    public class KijijiPropertyAdBot : HtmlBotBase, IPropertyAdBot
    {
        #region EVENTS
        public event PageLoadedEventHandler PageLoaded;

        #endregion
        #region PRIVATE MEMBERS
        private string fileDir;
        private string fileName;

        #endregion
        #region PUBLIC PROPERTIES
        public string BaseUrl { get; set; }
        public List<PropertyAdView> Ads { get; set; }
        public string CustomerCode { get; set; }
        public string Source { get; set; }
        public string StartPage { get; set; }
        public IHtmlPageNavigatorBot Navigator { get; set; }
        public IHtmlListBot SearchResults { get; set; }
        public int WaitInterval { get; set; }

        #endregion

        #region CONSTRUCTORS
        public KijijiPropertyAdBot()
        {
            Ads = new List<PropertyAdView>();
        }

        #endregion

        public void Initialize()
        {
            Navigator = new KijijiPageNavigatorBot() { BaseUrl = this.BaseUrl, WaitInterval = this.WaitInterval  };
            SearchResults = new KijijiAdListingBot() { BaseUrl = this.BaseUrl, WaitInterval = this.WaitInterval };
        }
        public PropertyAdView GetAdFromFile(string filePath)
        {
            var htmlContent = File.ReadAllText(filePath);
            return getAd(filePath, htmlContent);
        }
        public PropertyAdView GetAdFromPage(string url)
        {
            var htmlContent = HttpClient.GetContent(url);
            return getAd(url, htmlContent);
        }
        public void Run()
        {
            if (Navigator == null)
                throw new ArgumentNullException("Navigator");
            if (SearchResults == null)
                throw new ArgumentNullException("SearchResults");

            Navigator.LoadPage(StartPage);
            Navigator.LoadAllPages();
            SearchResults.LoadPages(Navigator.Pages);
            Ads = getAds(SearchResults.Items);
            var invalidAds = getInvalidAds(Ads);
            generateValidationReport(invalidAds);
            Ads = removeInvalidAds(Ads, invalidAds);
            //Ads = getLocation(Ads);
        }

        #region PRIVATE METHODS
        private void generateValidationReport(List<PropertyAdView> invalidAds)
        {
        }
        private PropertyAdView getAd(string url, string htmlContent)
        {
            PropertyAdView result = new PropertyAdView();
            var gbot = new GoogleTagBot();
            gbot.LoadHtml(htmlContent);
            result.AdCategory = getAdCategory(htmlContent);
            result.AdCode = StringConverter.ToInt(GetElementContent(htmlContent, "data-adId=\"", "\" "), -1);
            result.AdType = getAdType(htmlContent);
            result.Location = new Location()
                {
                    FormattedAddress = getAddress(htmlContent),
                    Locality = new GeoLocation() { Name = getCity(gbot.GetValue(gbot.PushScript, "city")) },
                    District = new GeoLocation() { Name = getDistrict(htmlContent) }
                };
            result.CustomerCode = this.CustomerCode;
            result.FullDescription = getDescription(htmlContent);
            result.IsAddressVisible = true;
            result.PropertyType = getPropertyType(gbot.GetValue(gbot.PushScript, "l2"));
            result.Title = GetElementContent(htmlContent, HtmlHead.TitleOpeningTag, HtmlHead.TitleClosingTag);
            result.Value = StringConverter.ToInt(gbot.GetValue(gbot.PushScript, "price"), -1);

            result.Details.Add(new PropertyAdDetailView()
            {
                Code = PropertyAdDetailCodes.PROPERTY_AD_UPDATE,
                Value = DateTime.Now.ToString("yyyy-MM-dd")
            });
            result.Details.Add(new PropertyAdDetailView()
            {
                Code = PropertyAdDetailCodes.PROPERTY_AD_URL,
                Value = url
            });
            result.Details.Add(new PropertyAdDetailView()
            {
                Code = PropertyAdDetailCodes.PROPERTY_AD_ROOMS,
                Value = gbot.GetValue(gbot.PushScript, "l3")
            });
            result.Details.Add(new PropertyAdDetailView()
            {
                Code = PropertyAdDetailCodes.PROPERTY_AD_SOURCE,
                Value = "Kijiji"
            });

            return result;
        }
        private PropertyAdCategories getAdCategory(string htmlContent)
        {
            return PropertyAdCategories.Residencial;
        }
        private string getAddress(string htmlContent)
        {
            string result = string.Empty;
            //var openTag = "<th>Adresse</th>";
            //var closeTag = "</td>";
            var aux = GetElementContent(htmlContent, "<th>Adresse</th>", "</td>");
            result = GetElementContent(aux, "<td>", "<br/>");
            return result;
        }
        private List<PropertyAdView> getAds(List<string> list)
        {
            AppOutput.Write("KijijiPropertyAdBot.getAds COUNT: " + list.Count.ToString(), true);
            var count = 0;
            var div = (int)list.Count / 20;
            List<PropertyAdView> result = new List<PropertyAdView>();
            foreach (var item in list)
            {
                count++;
                var ad = GetAdFromPage(item);
                //result.Add(ad);
                onPageLoaded(ad);
                System.Threading.Thread.Sleep(100);
                if (count % (div == 0 ? 1 : div) == 0)
                    AppOutput.Write("KijijiPropertyAdBot.getAds LOADING: " + ((count * 100) / list.Count).ToString() + "%", true);
            }
            AppOutput.Write("KijijiPropertyAdBot.getAds LOADING: 100%", true);
            return result;
        }

        private PropertyAdTypes getAdType(string htmlContent)
        {
            return PropertyAdTypes.Rent;
        }
        private string getCity(string city)
        {
            string result = city;
            switch (city)
            {
                case "quebeccity":
                    result = "Québec";
                    break;
            }
            return result;
        }
        private string getDistrict(string htmlContent)
        {
            return string.Empty;
        }
        private string getDescription(string htmlContent)
        {
            return string.Empty;
        }
        private List<PropertyAdView> getInvalidAds(List<PropertyAdView> ads)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();

            return result;
        }
        //private List<PropertyAdView> getLocation(List<PropertyAdView> ads)
        //{
        //    var result = new List<PropertyAdView>();
        //    foreach (var propertyAd in ads)
        //    {
        //        var location = getLocation(propertyAd);
        //        if (location != null)
        //        {
        //            propertyAd.Location = location;
        //            result.Add(propertyAd);
        //        }
        //    }
        //    return result;
        //}
        //private Location getLocation(PropertyAdView propertyAd)
        //{
        //    Location result = null;
        //    try
        //    {
        //        var address = WebHelper.GetUriValue(propertyAd.Location.AddressLine);
        //        if (!string.IsNullOrEmpty(address))
        //        {
        //            string locationsRequest = BingMapsRESTServices.CreateRequest(address);
        //            XDocument locationsResponse = BingMapsRESTServices.MakeRequest(locationsRequest);
        //            var response = BingMapsResponse.Parse(locationsResponse);
        //            if (response.HasLocations)
        //                result = response.Locations.FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return result;
        //}
        private string getPropertyType(string propertyType)
        {
            string result = propertyType;
            switch (propertyType)
            {
                case "appartements__condos":
                    result = "Appartements/Condos";
                    break;
            }
            return result;
        }
        private void onPageLoaded(PropertyAdView ad)
        {
            if (PageLoaded != null)
                PageLoaded(this, new PropertyAdEventArgs() { Ad = ad, CustomerCode = this.CustomerCode, Source = this.Source });
        }
        private List<PropertyAdView> removeInvalidAds(List<PropertyAdView> Ads, List<PropertyAdView> invalidAds)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            foreach (var item in invalidAds)
                Ads.Remove(item);
            return Ads;
        }

        #endregion
    }
}

