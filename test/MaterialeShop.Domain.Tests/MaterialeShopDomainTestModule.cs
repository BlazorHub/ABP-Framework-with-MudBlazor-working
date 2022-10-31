using MaterialeShop.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MaterialeShop;

[DependsOn(
    typeof(MaterialeShopEntityFrameworkCoreTestModule)
    )]
public class MaterialeShopDomainTestModule : AbpModule
{

}
