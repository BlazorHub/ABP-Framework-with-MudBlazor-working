using MaterialeShop.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MaterialeShop.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MaterialeShopEntityFrameworkCoreModule),
    typeof(MaterialeShopApplicationContractsModule)
)]
public class MaterialeShopDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
