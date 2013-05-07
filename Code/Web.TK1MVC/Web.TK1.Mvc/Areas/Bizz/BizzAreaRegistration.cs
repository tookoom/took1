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
                "Bizz_default",
                "Bizz/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
