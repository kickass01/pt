using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common.EntitySql;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Web.Http.OData.Routing.Conventions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using log4net.Util;

namespace PinkTravel.Helper
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ValidatedTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var message = GetMessage(htmlHelper, expression);
            return htmlHelper.TextBoxFor(expression, new Dictionary<string, object>
                {
                    { "data-val", "true" },
                    { "data-val-required", message ?? "Must not be empty"}
                });
        }

        public static MvcHtmlString ValidatedTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var message = GetMessage(htmlHelper, expression);
            return htmlHelper.TextAreaFor(expression, new Dictionary<string, object>
            {
                {"data-val", "true"},
                {"data-val-required", message ?? "Must not be empty"}
            });
        }

        private static string GetMessage<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            PropertyInfo[] props = metadata.ContainerType.GetProperties();

            string message = null;
            var info = props.First(prop => prop.Name == metadata.PropertyName);
            var attribute = info.GetCustomAttribute(typeof (RequiredAttribute)) as RequiredAttribute;
            if (attribute != null)
            {
                message = attribute.ErrorMessage;
                if (string.IsNullOrEmpty(message))
                {
                    var resource = attribute.ErrorMessageResourceType;
                    var resourceName = attribute.ErrorMessageResourceName;

                    if (resource == null || resourceName == null)
                        return null;

                    message = ResourcesPt.PinkTravel.ResourceManager.GetString(resourceName);
                }
            }

            return message;
        }

        public static MvcHtmlString ValidatedHiddenInputFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var message = GetMessage(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression, new Dictionary<string, object>
                {
                    { "data-val", "true" },
                    { "data-val-required", message ?? "Must not be empty"}
                });
        }
    }
}