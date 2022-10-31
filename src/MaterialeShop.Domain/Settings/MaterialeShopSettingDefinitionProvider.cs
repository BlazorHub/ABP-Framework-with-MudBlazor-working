using Volo.Abp.Settings;

namespace MaterialeShop.Settings;

public class MaterialeShopSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MaterialeShopSettings.MySetting1));
    }
}
