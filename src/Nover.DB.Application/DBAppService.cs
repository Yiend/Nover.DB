using Nover.DB.Localization;
using Volo.Abp.Application.Services;

namespace Nover.DB;

public abstract class DBAppService : ApplicationService
{
    protected DBAppService()
    {
        LocalizationResource = typeof(DBResource);
        ObjectMapperContext = typeof(NoverApplicationModule);
    }
}
