using Nover.DB.Core;
using Volo.Abp.Modularity;

namespace Nover.DB.Data;

[DependsOn(
    typeof(NoverCoreModule)  
)]
public class NoverDataModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}
