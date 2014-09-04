using System.Web.Mvc;
using TK1.Utility;

namespace Web.TK1.Mvc.Areas.Map
{
    public class MapAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Map";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Map_default",
                "{culture}/Map/{controller}/{action}/{id}",
                new { culture = "default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
