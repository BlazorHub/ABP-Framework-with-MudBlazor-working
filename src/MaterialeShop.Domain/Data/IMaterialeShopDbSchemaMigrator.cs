using System.Threading.Tasks;

namespace MaterialeShop.Data;

public interface IMaterialeShopDbSchemaMigrator
{
    Task MigrateAsync();
}
