using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MaterialeShop;

[Dependency(ReplaceServices = true)]
public class MaterialeShopBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MaterialeShop";
}
