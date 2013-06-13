using System.Web.Mvc;

namespace Web.TK1.Mvc.Areas.Bizz
{
    public class BizzAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Bizz";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Bizz_Broker_PropertyReleaseAd",
                "Bizz/{controller}/{action}/{customerCode}/Lancamento/{adCode}",
                new { action = "Imovel", customerCode = "",adType = "Release", adCode = 0 }
            );
            context.MapRoute(
                "Bizz_Broker_PropertyRentAd",
                "Bizz/{controller}/{action}/{customerCode}/Aluguel/{adCode}",
                new { action = "Imovel", customerCode = "", adType = "Rent", adCode = 0 }
            );
            context.MapRoute(
                "Bizz_Broker_PropertySellAd",
                "Bizz/{controller}/{action}/{customerCode}/Venda/{adCode}",
                new { action = "Imovel", customerCode = "", adType = "Sell", adCode = 0 }
            );

            context.MapRoute(
                "Bizz_Broker_PropertyAd",
                "Bizz/{controller}/{action}/{customerCode}/{adType}/{adCode}",
                new { action = "Imovel", customerCode = "", adType = "", adCode = 0 }
            );


            #region AJAX
            context.MapRoute(
                    "Bizz_Broker_Ajax_Cities",
                    "Bizz/{controller}/{action}/{customerCode}/{adType}",
                    new { action = "Cities", customerCode = "", adType = "" }
                ); 
            #endregion

            context.MapRoute(
                "Bizz_default",
                "Bizz/{controller}/{action}/{customerCode}",
                new { action = "Index", customerCode = UrlParameter.Optional }
            );
        }
    }
}
