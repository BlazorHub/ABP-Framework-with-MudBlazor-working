using MaterialeShop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace MaterialeShop.Permissions;

public class MaterialeShopPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MaterialeShopPermissions.GroupName);

        myGroup.AddPermission(MaterialeShopPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(MaterialeShopPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MaterialeShopPermissions.MyPermission1, L("Permission:MyPermission1"));

        var listaPermission = myGroup.AddPermission(MaterialeShopPermissions.Listas.Default, L("Permission:Listas"));
        listaPermission.AddChild(MaterialeShopPermissions.Listas.Create, L("Permission:Create"));
        listaPermission.AddChild(MaterialeShopPermissions.Listas.Edit, L("Permission:Edit"));
        listaPermission.AddChild(MaterialeShopPermissions.Listas.Delete, L("Permission:Delete"));

        var listaItemPermission = myGroup.AddPermission(MaterialeShopPermissions.ListaItems.Default, L("Permission:ListaItems"));
        listaItemPermission.AddChild(MaterialeShopPermissions.ListaItems.Create, L("Permission:Create"));
        listaItemPermission.AddChild(MaterialeShopPermissions.ListaItems.Edit, L("Permission:Edit"));
        listaItemPermission.AddChild(MaterialeShopPermissions.ListaItems.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MaterialeShopResource>(name);
    }
}