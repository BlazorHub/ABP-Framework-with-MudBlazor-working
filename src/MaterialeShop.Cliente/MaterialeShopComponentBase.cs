using MaterialeShop.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MaterialeShop.Cliente
{
    public abstract class MaterialeShopComponentBase : AbpComponentBase
    {
        protected MaterialeShopComponentBase()
        {
            LocalizationResource = typeof(MaterialeShopResource);
        }
    }
}