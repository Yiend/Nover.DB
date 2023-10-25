using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Nover.DB.WebView2
{
    /// <summary>
    /// Proxy service provider.
    /// </summary>
    public class ServiceLocator
    {
        private static ServiceLocator instance;

        /// <summary>
        /// Gets or sets the  global current <see cref="ServiceLocator"/> instance.
        /// </summary>
        public static ServiceLocator Current
        {
            get { return instance; }
            set { instance = value; }
        }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance.
        /// </summary>
        public virtual IServiceProvider Provider { get; private set; }

        /// <summary>
        /// Gets list of service objects by service <see cref="Type"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> instance.</param>
        /// <returns>List of objects.</returns>
        public virtual IEnumerable<object> GetInstances(Type serviceType)
        {
            if (Provider == null)
            {
                return null;
            }

            return Provider.GetServices(serviceType);
        }

        /// <summary>
        ///  Gets service object by service <see cref="Type"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>The object.</returns>
        public virtual object GetInstance(Type serviceType)
        {
            if (Provider == null)
            {
                return null;
            }

            return Provider.GetService(serviceType);
        }

        /// <summary>
        /// Gets list of service objects by service <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TService">Type of service.</typeparam>
        /// <returns>List of objects.</returns>
        public virtual IEnumerable<TService> GetInstances<TService>()
        {
            if (Provider == null)
            {
                return null;
            }

            return Provider.GetServices<TService>();
        }

        /// <summary>
        /// Gets service object by service <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TService">Type of service.</typeparam>
        /// <returns>The object</returns>
        public virtual TService GetInstance<TService>()
        {
            if (Provider == null)
            {
                return default(TService);
            }

            return Provider.GetService<TService>();
        }


        /// <summary>
        /// The function sets a new <see cref="ServiceLocator"/> instance from injected <see cref="IServiceProvider"/> instance.
        /// </summary>
        /// <param name="provider">The <see cref="IServiceProvider"/> instance.</param>
        public static void Bootstrap(IServiceProvider provider)
        {
            if (provider == null)
            {
                return;
            }

            if (Current != null)
            {
                var logger = provider.GetService<ILoggerFactory>()?.CreateLogger<ServiceLocator>();
                logger.LogWarning("ServiceLocator can only be created once.");
                return;
            }

            var serviceLocator = new ServiceLocator
            {
                Provider = provider
            };

            Current = serviceLocator;
        }
    }
}

