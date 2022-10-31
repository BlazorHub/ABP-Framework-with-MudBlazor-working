using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MaterialeShop.Data;

/* This is used if database provider does't define
 * IMaterialeShopDbSchemaMigrator implementation.
 */
public class NullMaterialeShopDbSchemaMigrator : IMaterialeShopDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
