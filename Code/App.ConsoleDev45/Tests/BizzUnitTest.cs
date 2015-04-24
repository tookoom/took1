using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Controller;
using TK1.Bizz;
//using TK1.Bizz.Net;
using TK1.Data.Bizz.Controller;
using TK1.Bizz.Broker.Bot;
using TK1.Html.Bot;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Broker.Controller;
using System.Net;
using System.Runtime.Serialization.Json;
using TK1.Net;
using TK1.Net.Services;
using System.Xml;
using System.Xml.Linq;
using TK1.Web;
using TK1.Data.Bizz.Model;
using TK1.Bizz.Mapper.Model;
using TK1.Data.Bizz.Mapper.Controller;
using TK1.Xml;
using System.IO;
using TK1.Utility;

namespace TK1.Dev.UnitTest
{
    public class BizzUnitTest
    {
        //private static string customerCode = "citymapper.ca";
        //private static string source = "kijiji";
        #region TESTS
        public static void BingTest()
        {
            //var url = @"http://dev.virtualearth.net/REST/v1/Locations/US/WA/Redmond/1%20Microsoft%20Way?output=xml&key=AnuuWOkXPOoOTQoMMDXgjjlHsVvGsEBcaCnA0xpFM2suSOlI-Sgc-4XtT2e4dkyu";
            //url = @"http://dev.virtualearth.net/REST/v1/Locations/CA/QC/g1r2c2/-/-?output=xml&includeNeighborhood=true&maxResults=10&key=AnuuWOkXPOoOTQoMMDXgjjlHsVvGsEBcaCnA0xpFM2suSOlI-Sgc-4XtT2e4dkyu";
            //var result = HttpClient.GetContent(url);
            var address = "3110 Boulevard Hochelaga, Québec, QC G1W 2R1, Canada";
            address = WebHelper.GetUriValue(address);

            string locationsRequest = BingMapsRESTServices.CreateRequest(address);
            XDocument locationsResponse = BingMapsRESTServices.MakeRequest(locationsRequest);
            //BingMapsRESTServices.ProcessResponse(locationsResponse);
            var response = BingMapsResponse.Parse(locationsResponse, true);
            var controller = new BizzMapperController();
            var location = response.Locations.FirstOrDefault();
            controller.UpdateLocationGroup(ref location);
            controller.UpdateLocationGroup(ref location);
        }
        public static void BrokerBotCleanupTest()
        {
            //cleanupDB();
        }
        public static void BrokerBotFileTest()
        {
            string customerCode = "citymapper.ca";
            string source = "kijiji";

            string adFilePath = @"D:\Projetos\TK1\Resources\TestFiles\BrokerBot\KijijiAd.txt";
            string searchFilePath = @"D:\Projetos\TK1\Resources\TestFiles\BrokerBot\KijijiSearch.txt";

            IPropertyAdBot bot = PropertyAdBotFactory.GetBotInstance(source);
            bot.Initialize();
            bot.Navigator.LoadFile(searchFilePath);
            bot.SearchResults.LoadFile(searchFilePath);
            var ad = bot.GetAdFromFile(adFilePath);
            var address = System.Uri.EscapeUriString(ad.Location.AddressLine);
            string locationsRequest = BingMapsRESTServices.CreateRequest(address);
            XDocument locationsResponse = BingMapsRESTServices.MakeRequest(locationsRequest);
            var response = BingMapsResponse.Parse(locationsResponse);
            if (response.HasLocations)
            {
                //ad.PropertyLatitude = response.Locations.FirstOrDefault().Latitude;
                //ad.PropertyLongitude = response.Locations.FirstOrDefault().Longitude;
            }
            updateDB(customerCode, source, new List<PropertyAdView>() { ad });
        }
        public static void BrokerBotFileFullTest()
        {
            string customerCode = "citymapper.ca";
            string source = "kijiji";
            string adFilePath = @"D:\Projetos\TK1\Resources\TestFiles\BrokerBot\KijijiAd.txt";
            string searchFilePath = @"D:\Projetos\TK1\Resources\TestFiles\BrokerBot\KijijiSearch.txt";

            var content = File.ReadAllText("Ads.xml");
            var ads = XmlSerializer<List<PropertyAdView>>.Load(content);
            updateDB(customerCode, source, ads);
        }
        public static void BrokerBotWebSingleTest()
        {
            string customerCode = "citymapper.ca";
            string source = "kijiji";
            string url = "http://www.kijiji.ca/v-appartement-condo-studio-1-1-2/ville-de-quebec/cartier-beau-1/597310947?src=topadsearch";

            IPropertyAdBot bot = PropertyAdBotFactory.GetBotInstance(source);
            bot.CustomerCode = customerCode;
            bot.WaitInterval = 1000;
            bot.Initialize();
            bot.StartPage = url;
            var ad = bot.GetAdFromPage(url);

            //var address = System.Uri.EscapeUriString(ad.Location.AddressLine);
            //string locationsRequest = BingMapsRESTServices.CreateRequest(address);
            //XDocument locationsResponse = BingMapsRESTServices.MakeRequest(locationsRequest);
            //var location = BingMapsResponse.Parse(locationsResponse);
            //ad.PropertyLatitude = location.Latitude;
            //ad.PropertyLongitude = location.Longitude;
            updateDB(customerCode, source, new List<PropertyAdView>() { ad });
            //CleanupDB();
        }
        internal static void BrokerDBTest()
        {
            List<PropertyAdView> ads = getAds();
            PropertyAdController controller = new PropertyAdController("kijiji");
            foreach (var item in ads)
            {
                controller.SetPropertyAd(item);
            }
        }
        public static void MailTest()
        {
            //new MailHelper().SendMail("teste", "teste", "andre.v.mattos@gmail.com;andre@tk1.net.br;andre.v.mattos@live.com;", true);
        }
        public static void UserTest()
        {
            UserController userController = new UserController();
            int userID = userController.GetUserID("andre", "senha");

            BizzUserController bizzUserController = new BizzUserController();
            var validUser = bizzUserController.CheckUserAccess(userID, AppNames.RealEstateBroker.ToString(), CustomerNames.Pandolfo.ToString(), AppRoles.Admin.ToString());
        }
        private static List<PropertyAdView> getAds()
        {
            string customerCode = "citymapper.ca";
            string source = "kijiji";

            List<PropertyAdView> ads = new List<PropertyAdView>();
            ads.Add(new PropertyAdView()
            {
                AdCategory = PropertyAdCategories.Residencial,
                AdCode = 1,
                AdType = PropertyAdTypes.Rent,
                Location = new Location()
                {
                    AddressLine = "G1S4H2",
                    Locality = new GeoLocation() { Name = "Québec" },
                    District = new GeoLocation() { Name = "Sillery" }
                },
                CustomerCode = customerCode,
                FullDescription = "Ad 1",
                IsAddressVisible = true,
                PropertyType = "House",
                Title = "Ad 1",
                Value = 530
            });
            ads.Add(new PropertyAdView()
            {
                AdCategory = PropertyAdCategories.Residencial,
                AdCode = 1,
                AdType = PropertyAdTypes.Rent,
                Location = new Location()
                {
                    AddressLine = "G1S4H2",
                    Locality = new GeoLocation() { Name = "Québec" },
                    District = new GeoLocation() { Name = "Montalm" }
                },
                CustomerCode = customerCode,
                FullDescription = "Ad 2",
                IsAddressVisible = true,
                PropertyType = "Appartements/Condos",
                Title = "Ad 2",
                Value = 750
            });
            return ads;
        }
        private static void updateDB(string customerCode, string source, List<PropertyAdView> ads)
        {
            var adController = new PropertyAdController(customerCode);
            var mapController = new BizzMapperController();
            var count = 0;
            var div = (int)ads.Count / 20;
            AppOutput.Write("runAdBot.UpdateDB COUNT: " + ads.Count.ToString(), true);
            foreach (var ad in ads)
            {
                count++;
                try
                {
                    var location = ad.Location;
                    mapController.UpdateLocation(ref location);
                    mapController.UpdateLocationGroup(ref location);
                    ad.Location = location;
                    adController.SetPropertyAd(ad);
                    adController.SetPropertyAdDetails(ad.AdType, ad.AdCode, ad.Details);
                    if (count % (div == 0 ? 1 : div) == 0)
                        AppOutput.Write("runAdBot.UpdateDB UPDATING:" + ((count * 100) / ads.Count).ToString() + "%", true);
                }
                catch (Exception exception)
                {
                    AppOutput.WriteToFile("runAdBot.UpdateDB ERROR: " + ad.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_URL).FirstOrDefault().Value, true);
                    AppOutput.WriteToFile(exception);
                }
                finally
                {

                }
            }
            AppOutput.Write("runAdBot.UpdateDB UPDATING: 100%", true);
        }

        #endregion
    }
}
