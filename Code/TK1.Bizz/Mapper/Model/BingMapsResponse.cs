using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TK1.Data.Converter;
using TK1.Bizz.Mapper.Model;

namespace TK1.Bizz.Mapper.Model
{
    public class BingMapsResponse
    {
        public bool HasLocations { get { return Locations == null ? false : Locations.Count() > 0; } }
        public List<Location> Locations { get; set; }

        public BingMapsResponse()
        {
            Locations = new List<Location>();
        }

        static public BingMapsResponse Parse(XDocument response)
        {
            return Parse(response, true);
        }
        static public BingMapsResponse Parse(XDocument xdoc, bool includeAddressInfo)
        {
            var response = new BingMapsResponse();
            var xn = getXmlNamespace();
            foreach (var item in xdoc.Descendants(xn + "Location"))
            {
                var location = new Location();
                location.Latitude = StringConverter.ToDouble(item.Descendants(xn + "Latitude").First().Value);
                location.Longitude = StringConverter.ToDouble(item.Descendants(xn + "Longitude").First().Value);
                location.Name = item.Descendants(xn + "Name").First().Value;

                if (includeAddressInfo)
                {
                    location.AddressLine = getValue(item, "AddressLine");
                    location.FormattedAddress = getValue(item, "FormattedAddress");
                    location.PostalCode = getValue(item, "PostalCode");

                    var aux = getValue(item, "AdminDistrict");
                    if(!string.IsNullOrEmpty(aux))
                        location.AdminDistrict = new GeoLocation() { Name = aux };

                    aux = getValue(item, "AdminDistrict2");
                    if (!string.IsNullOrEmpty(aux))
                        location.AdminSubDistrict = new GeoLocation() { Name = aux };

                    aux = getValue(item, "CountryRegion");
                    if (!string.IsNullOrEmpty(aux))
                        location.Country = new GeoLocation() { Name = aux };

                    aux = getValue(item, "Locality");
                    if (!string.IsNullOrEmpty(aux))
                        location.Locality = new GeoLocation() { Name = aux };

                    aux = getValue(item, "Neighborhood");
                    if (!string.IsNullOrEmpty(aux))
                        location.District = new GeoLocation() { Name = aux };
                }
                response.Locations.Add(location);
            }
            return response;
            ////Get formatted addresses: Option 1
            ////Get all locations in the response and then extract the formatted address for each location
            //XmlNodeList locationElements = locationsResponse.SelectNodes("//rest:Location", nsmgr);
            //if(locationElements.Count>0)
            //{
            //    var element = locationElements.FirstOrDefault();
            //}
            //Console.WriteLine("Show all formatted addresses: Option 1");
            //foreach (XmlNode location in locationElements)
            //{
            //    Console.WriteLine(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
            //}
            //Console.WriteLine();

            ////Get formatted addresses: Option 2
            ////Get all formatted addresses directly. This works because there is only one formatted address for each location.
            //XmlNodeList formattedAddressElements = locationsResponse.SelectNodes("//rest:FormattedAddress", nsmgr);
            //Console.WriteLine("Show all formatted addresses: Option 2");
            //foreach (XmlNode formattedAddress in formattedAddressElements)
            //{
            //    Console.WriteLine(formattedAddress.InnerText);
            //}
            //Console.WriteLine();

            ////Get the Geocode Points to use for display for each Location
            //XmlNodeList locationElementsForGP = locationsResponse.SelectNodes("//rest:Location", nsmgr);
            //Console.WriteLine("Show Goeocode Point Data");
            //foreach (XmlNode location in locationElements)
            //{
            //    XmlNodeList displayGeocodePoints = location.SelectNodes(".//rest:GeocodePoint/rest:UsageType[.='Display']/parent::node()", nsmgr);
            //    Console.Write(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
            //    Console.WriteLine(" has " + displayGeocodePoints.Count.ToString() + " display geocode point(s).");
            //}
            //Console.WriteLine();
            ////Get all locations that have a MatchCode=Good and Confidence=High
            //XmlNodeList matchCodeGoodElements = locationsResponse.SelectNodes("//rest:Location/rest:MatchCode[.='Good']/parent::node()", nsmgr);
            //Console.WriteLine("Show all addresses with MatchCode=Good and Confidence=High");
            //foreach (XmlNode location in matchCodeGoodElements)
            //{
            //    if (location.SelectSingleNode(".//rest:Confidence", nsmgr).InnerText == "High")
            //    {
            //        Console.WriteLine(location.SelectSingleNode(".//rest:FormattedAddress", nsmgr).InnerText);
            //    }
            //}

            //Console.WriteLine("Press any key to exit");
            //Console.ReadKey();
        }

        private static string getValue(XElement item, string p)
        {
            var result = string.Empty;
            if (item != null)
            {
                var xn = getXmlNamespace();
                var name = xn + p;
                if (item.Descendants(name).Count() > 0)
                    result = item.Descendants(name).First().Value;
            }
            return result;
        }

        private static XNamespace getXmlNamespace()
        {
            XNamespace xNamespace = XNamespace.Get("http://schemas.microsoft.com/search/local/ws/rest/v1");
            return xNamespace;

            ////Create namespace manager
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(locationsResponse.NameTable);
            //nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");
            //return nsmgr;
        }
    }
}
