using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TK1.Net.Services
{
    public class BingMapsRESTServices
    {
        static string BingMapsKey = "AnuuWOkXPOoOTQoMMDXgjjlHsVvGsEBcaCnA0xpFM2suSOlI-Sgc-4XtT2e4dkyu";

        //Create the request URL
        public static string CreateRequest(string queryString)
        {
            string UrlRequest = "http://dev.virtualearth.net/REST/v1/Locations/" +
                                           queryString +
                                           "?output=xml" +
                                           "&includeNeighborhood=true" +
                                           " &key=" + BingMapsKey;
            return (UrlRequest);
        }

        //Submit the HTTP Request and return the XML response
        public static XDocument MakeRequest(string requestUrl)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XDocument xmlDoc = XDocument.Load(response.GetResponseStream());
            return (xmlDoc);
        }

        static public void ProcessResponse(XmlDocument locationsResponse)
        {
            //Create namespace manager
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(locationsResponse.NameTable);
            nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");

            //Get formatted addresses: Option 1
            //Get all locations in the response and then extract the formatted address for each location
            XmlNodeList locationElements = locationsResponse.SelectNodes("//rest:Location", nsmgr);
            Console.WriteLine("Show all formatted addresses: Option 1");
            foreach (XmlNode location in locationElements)
            {
                Console.WriteLine(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
            }
            Console.WriteLine();

            //Get formatted addresses: Option 2
            //Get all formatted addresses directly. This works because there is only one formatted address for each location.
            XmlNodeList formattedAddressElements = locationsResponse.SelectNodes("//rest:FormattedAddress", nsmgr);
            Console.WriteLine("Show all formatted addresses: Option 2");
            foreach (XmlNode formattedAddress in formattedAddressElements)
            {
                Console.WriteLine(formattedAddress.InnerText);
            }
            Console.WriteLine();

            //Get the Geocode Points to use for display for each Location
            XmlNodeList locationElementsForGP = locationsResponse.SelectNodes("//rest:Location", nsmgr);
            Console.WriteLine("Show Goeocode Point Data");
            foreach (XmlNode location in locationElements)
            {
                XmlNodeList displayGeocodePoints = location.SelectNodes(".//rest:GeocodePoint/rest:UsageType[.='Display']/parent::node()", nsmgr);
                Console.Write(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
                Console.WriteLine(" has " + displayGeocodePoints.Count.ToString() + " display geocode point(s).");
            }
            Console.WriteLine();
            //Get all locations that have a MatchCode=Good and Confidence=High
            XmlNodeList matchCodeGoodElements = locationsResponse.SelectNodes("//rest:Location/rest:MatchCode[.='Good']/parent::node()", nsmgr);
            Console.WriteLine("Show all addresses with MatchCode=Good and Confidence=High");
            foreach (XmlNode location in matchCodeGoodElements)
            {
                if (location.SelectSingleNode(".//rest:Confidence", nsmgr).InnerText == "High")
                {
                    Console.WriteLine(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
