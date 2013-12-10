using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PinkTravel.Localization.ResourceProviders
{
    public class LocalizationResourceProvider
    {
        private static ILocalizationResourceProvider _instance;
        private static Func<ILocalizationResourceProvider> _initializer;
        private static Func<string> _cultureNameResolver;
        private static object _lock = new object();

        public static ILocalizationResourceProvider Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            if (_initializer == null)
                            {
                                throw new ArgumentNullException(paramName: "_initializer");
                            }

                            _instance = _initializer.Invoke();
                        }
                    }
                }

                return _instance;
            }
        }

        public static string CultureName
        {
            get
            {
                return _cultureNameResolver.Invoke();
            }
        }

        private LocalizationResourceProvider()
        {
        }

        internal static void RegisterCultureNameResolver(Func<string> cultureNameResolver)
        {
            _cultureNameResolver = cultureNameResolver;
        }

        internal static void RegisterResourceProvider(Func<ILocalizationResourceProvider> initializer)
        {
            _initializer = initializer;
        }

    }
}
