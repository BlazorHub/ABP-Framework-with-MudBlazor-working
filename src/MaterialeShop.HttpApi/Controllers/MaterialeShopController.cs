using MaterialeShop.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MaterialeShop.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MaterialeShopController : AbpControllerBase
{
    protected MaterialeShopController()
    {
        LocalizationResource = typeof(MaterialeShopResource);
    }
}
