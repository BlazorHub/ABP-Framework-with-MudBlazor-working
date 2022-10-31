using Volo.Abp.Modularity;

namespace MaterialeShop;

[DependsOn(
    typeof(MaterialeShopApplicationModule),
    typeof(MaterialeShopDomainTestModule)
    )]
public class MaterialeShopApplicationTestModule : AbpModule
{

}
