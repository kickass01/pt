using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PinkTravel.Localization
{
    public class LocalizationControllerHelper
    {
        public static void OnBeginExecuteCore(Controller controller)
        {
            var routeValues = controller.RouteData.Values;
            string langHeader;
            if (!string.IsNullOrEmpty(routeValues[Constants.LocalizationRouteParameter] as string))
            {
                langHeader = routeValues[Constants.LocalizationRouteParameter].ToString();
                try
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                var cookie = controller.HttpContext.Request.Cookies[Constants.LocalizationCookieName];

                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    langHeader = cookie.Value;
                    try
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    langHeader = controller.HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }

                controller.RouteData.Values[Constants.LocalizationRouteParameter] = langHeader;
            }

            var languageCookie = new HttpCookie(Constants.LocalizationCookieName);
            languageCookie.Value = langHeader;
            languageCookie.Expires = DateTime.Now.AddDays(60);
            controller.Response.SetCookie(languageCookie);
        }
    }
}