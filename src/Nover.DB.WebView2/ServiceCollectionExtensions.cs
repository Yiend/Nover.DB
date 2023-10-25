using Microsoft.Extensions.Logging;
using Nover.DB.WebView2.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyFullPath"></param>
        public static void AddControllers(this IServiceCollection services, string assemblyFullPath)
        {
            if (string.IsNullOrWhiteSpace(assemblyFullPath))
            {
                return;
            }

            if (File.Exists(assemblyFullPath))
            {
                var assembly = Assembly.LoadFrom(assemblyFullPath);
                services.AddControllers(assembly);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddControllers(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }

            try
            {
                services.RegisterAssembly(assembly, ServiceLifetime.Singleton);
            }
            catch (Exception exception)
            {
                var logger = services.GetServiceLazy<ILogger>();
                logger?.Value?.LogError(exception);
            }
        }
    }
}
