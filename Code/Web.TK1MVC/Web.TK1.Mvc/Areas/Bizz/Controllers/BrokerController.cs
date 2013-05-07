using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.TK1.Mvc.Areas.Bizz.Controllers
{
    public class BrokerController : Controller
    {
        //
        // GET: /Bizz/Broker/

        public ActionResult Index(string customerCode)
        {
            return View(customerCode);
        }

        public ActionResult Destaques(string customerCode)
        {
            return View(customerCode);
        }

        public ActionResult Imovel(string customerCode, int adType, int adCode)
        {
            return View();
        }


    }
}
