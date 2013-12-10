using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using PinkTravel.Localization.ModelProviders;
using PinkTravel.Localization.ResourceProviders;

namespace PinkTravel.Localization
{
    public static class LocalizationConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Manage" }
            );

            routes.MapRoute(
                name: "Images",
                url: "PImages/{action}/{name}",
                defaults: new { controller = "PImages", action = "Image", name = ""}
            );

            routes.MapRoute(
                Constants.LocalizationRouteName, // Route name
                string.Format("{{{0}}}/{{controller}}/{{action}}/{{id}}", Constants.LocalizationRouteParameter), // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        public static void RegisterResourceProvider(Func<ILocalizationResourceProvider> initializer)
        {
            LocalizationResourceProvider.RegisterResourceProvider(initializer);
            LocalizationResourceProvider.RegisterCultureNameResolver(() => Thread.CurrentThread.CurrentUICulture.Name);
        }

        public static void RegisterResourceProvider(Func<ILocalizationResourceProvider> initializer, Func<string> cultureNameResolver)
        {
            LocalizationResourceProvider.RegisterResourceProvider(initializer);
            LocalizationResourceProvider.RegisterCultureNameResolver(cultureNameResolver);
        }

        public static void RegisterModelProviders()
        {
            // register the model metadata provider
            ModelMetadataProviders.Current = new LocalizableDataAnnotationsModelMetadataProvider();

            // register the model validation provider
            var provider = ModelValidatorProviders.Providers.FirstOrDefault(p => p.GetType() == typeof(DataAnnotationsModelValidatorProvider));
            if (provider != null)
            {
                ModelValidatorProviders.Providers.Remove(provider);
            }

            provider = new LocalizableDataAnnotationsModelValidatorProvider();
            ModelValidatorProviders.Providers.Add(provider);
        }
    }
}
