using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace PinkTravel.Localization
{
    public static class LanguageBarHelper
    {
        private class Language
        {
            public string Url { get; set; }

            public string ActionName { get; set; }

            public string ControllerName { get; set; }

            public RouteValueDictionary RouteValues { get; set; }

            public bool IsSelected { get; set; }

            public MvcHtmlString HtmlSafeUrl
            {
                get
                {
                    return MvcHtmlString.Create(Url);
                }
            }
        }

        private static Language LanguageUrl(this HtmlHelper helper, string cultureName, bool strictSelected = false)
        {
            cultureName = cultureName.ToLower();

            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
            var queryString = helper.ViewContext.HttpContext.Request.QueryString;

            foreach (string key in queryString.Cast<string>().Where(key => queryString[key] != null && !string.IsNullOrWhiteSpace(key)))
            {
                if (routeValues.ContainsKey(key))
                {
                    routeValues[key] = queryString[key];
                }
                else
                {
                    routeValues.Add(key, queryString[key]);
                }
            }

            var actionname = routeValues["action"].ToString();
            var controllerName = routeValues["controller"].ToString();

            routeValues[Constants.LocalizationRouteParameter] = cultureName;
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            var currentLangName = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
            var url = urlHelper.RouteUrl(Constants.LocalizationRouteName, routeValues);
            var isSelected = strictSelected ? currentLangName == cultureName : currentLangName.StartsWith(cultureName);
            return new Language()
            {
                Url = url,
                ActionName = actionname,
                ControllerName = controllerName,
                RouteValues = routeValues,
                IsSelected = isSelected
            };
        }

        public static MvcHtmlString LanguageSelectorLink(this HtmlHelper helper, string cultureName, string selectedText,
            string unselectedText, IDictionary<string, object> htmlAttributes, bool strictSelected = false)
        {
            var language = LanguageUrl(helper, cultureName, strictSelected);
            var link = helper.RouteLink(language.IsSelected ? selectedText : unselectedText,
                Constants.LocalizationRouteName, language.RouteValues, htmlAttributes);

            return link;
        }
    }
}