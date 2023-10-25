using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Nover.DB.Core;
using Nover.DB.WebView2.Browser;
using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2.Defaults;
using Nover.DB.WebView2.Infrastructure;
using Nover.DB.WebView2.NativeHosts;
using Nover.DB.WebView2.Network;
using Nover.DB.WebView2.Owin;
using System;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Nover.DB.WebView2;

[DependsOn(typeof(NoverCoreModule))]
public class NoverWebView2Module : AbpModule
{
    private static bool _servicesConfigured;
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddSingleton<IErrorHandler, ErrorHandler>();
        context.Services.TryAddSingleton<IDataTransferOptions, DataTransferOptions>();
        context.Services.TryAddSingleton<IConfiguration, Defaults.Configuration>();
        context.Services.TryAddSingleton<IAppSettings, AppSettings>();
        context.Services.TryAddSingleton<IErrorHandler, ErrorHandler>();
        context.Services.TryAddSingleton<IDataTransferOptions, DataTransferOptions>();
        context.Services.TryAddSingleton<IActionParameterBinder, ActionParameterBinder>();
        context.Services.TryAddSingleton<IScriptExecutor, ScriptExecutor>();
        context.Services.TryAddSingleton<IActionRouteProvider, ActionRouteProvider>();
        context.Services.TryAddSingleton<IHostObjectProvider, HostObjectProvider>();
        context.Services.TryAddSingleton<IActionControllerProvider, ActionControllerProvider>();
        context.Services.TryAddSingleton<IResourceRequestSchemeHandler, ResourceRequestSchemeHandler>();
        context.Services.TryAddSingleton<IRouteToActionSchemeHandler, RouteToActionSchemeHandler>();
        context.Services.TryAddSingleton<IOwinPipeline, OwinPipeline>();
        context.Services.TryAddSingleton<IResourceRequestHandler, ResourceRequestHandler>();

        context.Services.TryAddSingleton<IBrowserWindow, BrowserWindow>();
        context.Services.TryAddSingleton<IWindowController, WindowController>();

        var platform = HostRuntime.Platform;
        switch (platform)
        {
            case Platform.MacOSX:
                throw new NotSupportedException("No support for MacOS yet.");

            case Platform.Linux:
                throw new NotSupportedException("No support for Linux yet.");

            case Platform.Windows:
                context.Services.TryAddSingleton<INativeHost, WinNativeHost>();
                break;

            default:
                context.Services.TryAddSingleton<INativeHost, WinNativeHost>();
                break;
        }
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
        var looger = context.ServiceProvider.GetService<ILogger<NoverWebView2Module>>();
        if (_servicesConfigured)
        {
            looger?.LogWarning("Services must be configured before application is initialized.");
            return;
        }

        #region Configuration
        var config = context.ServiceProvider.GetService<IConfiguration>();
        if (config == null)
        {
            config = new Defaults.Configuration();
        }

        #endregion Configuration

        #region Application/User Settings

        var appSettings = context.ServiceProvider.GetService<IAppSettings>();
        if (appSettings == null)
        {
            appSettings = new AppSettings();
        }

        var currentAppSettings = new CurrentAppSettings
        {
            Properties = appSettings
        };
        AppUser.App = currentAppSettings;
        AppUser.App.Properties.Read(config);

        #endregion

        _servicesConfigured = true;

    }
}
