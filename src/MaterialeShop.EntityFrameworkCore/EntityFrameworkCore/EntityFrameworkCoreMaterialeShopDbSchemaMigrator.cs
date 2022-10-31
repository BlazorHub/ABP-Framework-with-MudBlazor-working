using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MaterialeShop.Data;
using Volo.Abp.DependencyInjection;

namespace MaterialeShop.EntityFrameworkCore;

public class EntityFrameworkCoreMaterialeShopDbSchemaMigrator
    : IMaterialeShopDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMaterialeShopDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MaterialeShopDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MaterialeShopDbContext>()
            .Database
            .MigrateAsync();
    }
}
