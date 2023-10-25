using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Nover.DB.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;

namespace Nover.DB.Core
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpValidationModule),
        typeof(AbpDddDomainSharedModule)
    )]
    public class NoverCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NoverCoreModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DBResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/DB");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("DB", typeof(DBResource));
            });
        }
    }

}


