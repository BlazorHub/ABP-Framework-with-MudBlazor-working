using MaterialeShop.Localization;
using Volo.Abp.Application.Services;

namespace MaterialeShop;

/* Inherit your application services from this class.
 */
public abstract class MaterialeShopAppService : ApplicationService
{
    protected MaterialeShopAppService()
    {
        LocalizationResource = typeof(MaterialeShopResource);
    }
}
