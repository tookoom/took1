using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.PropertyAdBot.Model;
using TK1.Bizz;
using TK1.Bizz.Broker.Bot;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Broker.Controller;
using TK1.Data.Bizz.Mapper.Controller;
using TK1.Data.Bizz.Model;
using TK1.Utility;

namespace App.PropertyAdBot
{
    class Program
    {
        private static PropertyAdLoader loader = new PropertyAdLoader();

        static void Main(string[] args)
        {
            AppOutput.LogFileDirectory = @"D:\_TEMP\LOGS";
            AppOutput.LogFileName = @"TK1.App.PropertyAdBot";
            AppOutput.WriteConsole = true;
            AppOutput.WriteFile = true;
            AppOutput.WriteExecutionStart();

            loader.StartAsyncLoad();

            try
            {
                var urls = new List<string>();

                urls.Add("http://www.kijiji.ca/b-appartement-condo-studio-1-1-2/ville-de-quebec/c211l1700124?ad=offering");
                urls.Add("http://www.kijiji.ca/b-appartement-condo-studio-2-1-2/ville-de-quebec/c212l1700124?ad=offering");
                urls.Add("http://www.kijiji.ca/b-appartement-condo-3-1-2/ville-de-quebec/c213l1700124?ad=offering");
                urls.Add("http://www.kijiji.ca/b-appartement-condo-4-1-2/ville-de-quebec/c214l1700124?ad=offering");
                urls.Add("http://www.kijiji.ca/b-appartement-condo-5-1-2/ville-de-quebec/c215l1700124?ad=offering");
                urls.Add("http://www.kijiji.ca/b-appartement-condo-6-1-2-et-plus/ville-de-quebec/c216l1700124?ad=offering");

                //urls.Add("http://www.kijiji.ca/b-appartement-condo-studio-1-1-2/grand-montreal/c211l80002?ad=offering");
                //urls.Add("http://www.kijiji.ca/b-appartement-condo-studio-2-1-2/grand-montreal/c212l80002?ad=offering");
                //urls.Add("http://www.kijiji.ca/b-appartement-condo-3-1-2/grand-montreal/c213l80002?ad=offering");
                //urls.Add("http://www.kijiji.ca/b-appartement-condo-4-1-2/grand-montreal/c214l80002?ad=offering");
                //urls.Add("http://www.kijiji.ca/b-appartement-condo-5-1-2/grand-montreal/c215l80002?ad=offering");
                //urls.Add("http://www.kijiji.ca/b-appartement-condo-6-1-2-et-plus/grand-montreal/c216l80002?ad=offering");

                foreach (var url in urls)
                {
                    AppOutput.Write("runAdBot: " + url, true);
                    runAdBot("citymapper.ca", "kijiji", url);
                }
            }
            catch (Exception exception)
            {
                AppOutput.Write(exception);
            }
            finally
            {
                loader.StopAsyncLoad();
                loader.WaitForAsyncOperations();
                AppOutput.WriteExecutionEnd();
                Console.ReadKey();
            }
        }
        private static void cleanupDB()
        {
            string customerCode = "citymapper.ca";
            string source = "kijiji";
            var controller = new PropertyAdController(customerCode);
            controller.CleanUp();
        }
        private static void runAdBot(string customerCode, string source, string url)
        {
            IPropertyAdBot bot = PropertyAdBotFactory.GetBotInstance(source);
            bot.CustomerCode = customerCode;
            bot.WaitInterval = 500;
            bot.Initialize();
            bot.StartPage = url;
            bot.PageLoaded += new PageLoadedEventHandler(onPageLoaded);
            AppOutput.Write("runAdBot.Run BEGIN: " + url, true);
            bot.Run();
            //try
            //{
            //    var content = XmlSerializer<List<PropertyAdView>>.Save(bot.Ads);
            //    File.WriteAllText(string.Format("{0}.xml", url), content);
            //}
            //catch (Exception)
            //{
            //}
            AppOutput.Write("runAdBot.Run END: ", true);
            //updateDB(customerCode, source, bot.Ads);
        }
        //private static void updateDB(string customerCode, string source, PropertyAdView ad)
        //{
        //    var adController = new PropertyAdController(customerCode);
        //    var mapController = new BizzMapperController();
        //    try
        //    {
        //        var location = ad.Location;
        //        mapController.UpdateLocation(ref location);
        //        mapController.UpdateLocationGroup(ref location);
        //        ad.Location = location;
        //        adController.SetPropertyAd(ad);
        //        adController.SetPropertyAdDetails(ad.AdType, ad.AdCode, ad.Details);
        //    }
        //    catch (Exception exception)
        //    {
        //        AppOutput.WriteToFile("runAdBot.UpdateDB ERROR: " + ad.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_URL).FirstOrDefault().Value, true);
        //        AppOutput.WriteToFile(exception);
        //    }
        //    finally
        //    {

        //    }
        //}
        //private static void updateDB(string customerCode, string source, List<PropertyAdView> ads)
        //{
        //    var adController = new PropertyAdController(customerCode);
        //    var mapController = new BizzMapperController();
        //    var count = 0;
        //    var div = (int)ads.Count / 20;
        //    AppOutput.Write("runAdBot.UpdateDB COUNT: " + ads.Count.ToString(), true);
        //    foreach (var ad in ads)
        //    {
        //        count++;
        //        try
        //        {
        //            var location = ad.Location;
        //            mapController.UpdateLocation(ref location);
        //            mapController.UpdateLocationGroup(ref location);
        //            ad.Location = location;
        //            adController.SetPropertyAd(ad);
        //            adController.SetPropertyAdDetails(ad.AdType, ad.AdCode, ad.Details);
        //            if (count % (div == 0 ? 1 : div) == 0)
        //                AppOutput.Write("runAdBot.UpdateDB UPDATING:" + ((count * 100) / ads.Count).ToString() + "%", true);
        //        }
        //        catch (Exception exception)
        //        {
        //            AppOutput.WriteToFile("runAdBot.UpdateDB ERROR: " + ad.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_URL).FirstOrDefault().Value, true);
        //            AppOutput.WriteToFile(exception);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    AppOutput.Write("runAdBot.UpdateDB UPDATING: 100%", true);
        //}
        private static void onPageLoaded(object sender, PropertyAdEventArgs e)
        {
            if (e != null && e.Ad != null)
            {
                loader.LoadAsync(new PropertyAdLoadItem()
                {
                    Ad = e.Ad,
                    CustomerCode = e.CustomerCode,
                    Source = e.Source
                });
                //updateDB(e.CustomerCode, e.Source, e.Ad);
            }
        }
    }
}
