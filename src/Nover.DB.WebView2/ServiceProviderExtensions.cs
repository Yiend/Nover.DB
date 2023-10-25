using Microsoft.Extensions.Logging;
using Nover.DB.WebView2;
using Nover.DB.WebView2.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        private static bool _servicesInitialized;

        /// <summary>
        /// Runs the application after build.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void WebViewRun(this IServiceProvider serviceProvider, string[] args)
        {
            ServiceLocator.Bootstrap(serviceProvider);

            var windowController = serviceProvider.GetService<IWindowController>();
            try
            {
                windowController.Run(args);
            }
            finally
            {
                windowController.Dispose();
                (serviceProvider as ServiceProvider)?.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="Exception"></exception>
        public static void UseRouting(this IServiceProvider services)
        {
            var looger = services.GetService<ILoggerFactory>()?.CreateLogger<NoverWebView2Module>();
            if (_servicesInitialized)
            {
                looger?.LogWarning("Services must be initialized before controller assemblies are scanned.");
                return;
            }

            var routeProvider = services.GetService<IActionRouteProvider>();
            if (routeProvider != null)
            {
                var controllers = services.GetServices<ActionController>();
                routeProvider.RegisterAllRoutes(controllers?.ToList());
            }

            _servicesInitialized = true;
        }
    }
}
