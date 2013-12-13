using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using PinkTravel.Localization.ResourceProviders;

namespace PinkTravel.Localization
{
    public class LocalizationGlobalResourceProvider : LocalizationResourceProviderBase
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationGlobalResourceProvider()
        {
            //_resourceManager = new ResourceManager("PinkTravel", Assembly.GetAssembly(this.GetType()));
            _resourceManager = ResourcesPt.PinkTravel.ResourceManager;
        }

        protected override string OnGetString(string cultureName, string key)
        {
            return _resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(cultureName));
        }
    }
}