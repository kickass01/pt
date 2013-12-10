using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PinkTravel.Filters;
using PinkTravel.Localization.ResourceProviders;

namespace PinkTravel.Controllers
{
    public class HomeController : LocalizedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = LocalizationResourceProvider.Current.GetString("Title");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
