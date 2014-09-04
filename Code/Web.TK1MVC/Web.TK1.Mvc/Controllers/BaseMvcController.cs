using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TK1.Utility;

namespace Web.TK1.Mvc.Controllers
{
    public class BaseMvcController : Controller
    {
        public bool IgnoreCultureChange { get; set; }

        public BaseMvcController()
        {
            IgnoreCultureChange = false;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cookie = Request.Cookies["_culture"];
            var culture = string.Empty;

            var cookieCulture = cookie == null ? null : cookie.Value as string;
            var routeCulture = RouteData.Values["culture"] as string;

            if (routeCulture == "default")
                routeCulture = null;

            if (string.IsNullOrEmpty(cookieCulture))
            {
                if (string.IsNullOrEmpty(routeCulture))
                    culture = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
                else
                    culture = routeCulture;
            }
            else
            {
                if (!IgnoreCultureChange && (string.IsNullOrEmpty(routeCulture) ? cookieCulture : routeCulture) != cookieCulture)
                    culture = routeCulture;
                else
                    culture = cookieCulture;
            }

            // Validate culture name
            culture = CultureHelper.GetImplementedCulture(culture); // This is safe


            if (!IgnoreCultureChange && routeCulture != culture)
            {
                // Force a valid culture in the URL
                RouteData.Values["culture"] = culture.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            if (!IgnoreCultureChange)
            {
                if(cookie== null)
                    cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }


            return base.BeginExecuteCore(callback, state);
        }


    }
}
