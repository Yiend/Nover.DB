using Nover.DB.Core;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Nover.DB;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(NoverCoreModule)
)]
public class NoverDomainModule : AbpModule
{

}
