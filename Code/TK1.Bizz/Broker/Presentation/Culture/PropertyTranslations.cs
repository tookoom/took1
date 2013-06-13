using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Broker.Presentation.Culture
{
    public class PropertyTranslations
    {
        public static string GetAdTypeDisplayName(PropertyAdTypes adType, string culture) 
        {
            string result = string.Empty;
            switch (adType)
            {
                case Presentation.PropertyAdTypes.Release:
                    result = "Lançamento";
                    break;
                case Presentation.PropertyAdTypes.Rent:
                    result = "Aluguel";
                    break;
                case Presentation.PropertyAdTypes.Sell:
                    result = "Venda";
                    break;
            }
            return result;

        }
        public static PropertyAdTypes GetAdTypeFromDisplayName(string adType, PropertyAdTypes defaultValue)
        {
            PropertyAdTypes result = defaultValue;
            if(adType == null)
                adType = string.Empty;
            switch (adType.ToUpper())
            {
                case"LANÇAMENTO":
                case"LANCAMENTO":
                case"RELEASE":
                    result = PropertyAdTypes.Release;
                    break;
                case "ALUGUEL":
                case"RENT":
                    result = PropertyAdTypes.Rent;
                    break;
                case "VENDA":
                case"SELL":
                    result = PropertyAdTypes.Sell;
                    break;
            }
            return result;
        }
        public static string GetRoomDisplayName(string propertyType, string uiCulture)
        {
            if (propertyType == null)
                throw new ArgumentNullException("Parameter propertyType can't be null");

            string result = string.Empty;
            switch (propertyType.ToUpper())
            {
                case "APARTAMENTO":
                case "CASA":
                case "CASA EM CONDOMINIO":
                case "CASA EM CONDOMÍNIO":
                    result = "Dormitório";
                    break;
                case "":
                    result = string.Empty;
                    break;
            }
            return result;
        }
        public static string GetRoomDisplayName(string propertyType, int roomQuantity, string uiCulture)
        {
            if (propertyType == null)
                throw new ArgumentNullException("Parameter propertyType can't be null");

            string result = string.Empty;
            switch (propertyType.ToUpper())
            {
                case "APARTAMENTO":
                case "CASA":
                case "CASA EM CONDOMINIO":
                case "CASA EM CONDOMÍNIO":
                    result = "Dormitório";
                    break;
                case "":
                    result = string.Empty;
                    break;
            }
            if (roomQuantity > 1 & result != string.Empty)
                result += "s";
            return result;
        }
        public static string GetRoomQuantityDisplayName(string propertyType, int roomQuantity, string uiCulture)
        {
            if (propertyType == null)
                throw new ArgumentNullException("Parameter propertyType can't be null");

            var result = string.Empty;
            var roomName = string.Empty;

            switch (propertyType.ToUpper())
            {
                case "APARTAMENTO":
                case "CASA":
                case "CASA EM CONDOMINIO":
                case "CASA EM CONDOMÍNIO":
                    roomName = "dormitório";
                    break;
                case "":
                    roomName = string.Empty;
                    break;
            }

            if (roomQuantity > 1)
                roomName += "s";
            result = string.Format("{0} {1}", roomQuantity, roomName);
            return result;
        }
        public static string GetAreaDisplayName(float area, string uiCulture)
        {
            string result = string.Empty;
            result = string.Format("{0} {1}", area, "m²");
            return result;
        }
    }
}
