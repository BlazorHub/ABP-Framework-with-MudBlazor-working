using MaterialeShop.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MaterialeShop.Admin
{
    public abstract class MaterialeShopComponentBase : AbpComponentBase
    {
        protected MaterialeShopComponentBase()
        {
            LocalizationResource = typeof(MaterialeShopResource);
        }
    }
}