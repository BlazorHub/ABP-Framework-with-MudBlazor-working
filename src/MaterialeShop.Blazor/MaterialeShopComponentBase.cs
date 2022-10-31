using MaterialeShop.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MaterialeShop.Blazor;

public abstract class MaterialeShopComponentBase : AbpComponentBase
{
    protected MaterialeShopComponentBase()
    {
        LocalizationResource = typeof(MaterialeShopResource);
    }
}
