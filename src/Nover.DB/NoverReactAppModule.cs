using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Nover.DB.ReactApp.Controllers;
using Nover.DB.WebView2.Configuration;
using Nover.DB.WebView2;
using System;
using Volo.Abp.Timing;

namespace Nover.DB.ReactApp;

[DependsOn(typeof(NoverWebView2Module))]
public class NoverReactAppModule : AbpModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddControllers(typeof(BaseController).Assembly);
        context.Services.AddSingleton<IConfiguration, ReactAppConfig>();
        context.Services.AddSingleton<IBrowserWindow, ReactAppWindow>();


        ConfigureOptions();
    }


    /// <summary>
    /// 
    /// </summary>
    private void ConfigureOptions()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
        });

        Configure<AbpClockOptions>(options => options.Kind = DateTimeKind.Local);

       
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.ServiceProvider.UseRouting();
    }
}
