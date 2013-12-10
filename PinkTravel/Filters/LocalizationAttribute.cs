using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace PinkTravel.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["lang"] != null && !string.IsNullOrWhiteSpace(filterContext.RouteData.Values["lang"].ToString()))
            {
                var language = filterContext.RouteData.Values["lang"].ToString();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
            }
            else
            {
                var cookie = filterContext.HttpContext.Request.Cookies["language"];
                string langHeader;

                if (cookie != null)
                {
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    langHeader = filterContext.HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }

                filterContext.RouteData.Values["lang"] = langHeader;
            }

            var languageCookie = new HttpCookie("language");
            languageCookie.Expires = DateTime.Now.AddDays(60);
            filterContext.HttpContext.Response.SetCookie(languageCookie);

            base.OnActionExecuting(filterContext);
        }
    }
}