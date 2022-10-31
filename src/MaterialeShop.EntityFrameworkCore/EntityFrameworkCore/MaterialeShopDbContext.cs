using MaterialeShop.Enderecos;
using MaterialeShop.ListaItems;
using MaterialeShop.Listas;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace MaterialeShop.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class MaterialeShopDbContext :
    AbpDbContext<MaterialeShopDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<ListaItem> ListaItems { get; set; }
    public DbSet<Lista> Listas { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MaterialeShopDbContext(DbContextOptions<MaterialeShopDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(MaterialeShopConsts.DbTablePrefix + "YourEntities", MaterialeShopConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {
            builder.Entity<Lista>(b =>
{
    b.ToTable(MaterialeShopConsts.DbTablePrefix + "Listas", MaterialeShopConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Titulo).HasColumnName(nameof(Lista.Titulo)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ListaItem>(b =>
{
    b.ToTable(MaterialeShopConsts.DbTablePrefix + "ListaItems", MaterialeShopConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Descricao).HasColumnName(nameof(ListaItem.Descricao)).IsRequired();
    b.Property(x => x.Quantidade).HasColumnName(nameof(ListaItem.Quantidade));
    b.Property(x => x.UnidadeMedida).HasColumnName(nameof(ListaItem.UnidadeMedida));
    b.HasOne<Lista>().WithMany().IsRequired().HasForeignKey(x => x.ListaId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Endereco>(b =>
{
    b.ToTable(MaterialeShopConsts.DbTablePrefix + "Enderecos", MaterialeShopConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.EnderecoCompleto).HasColumnName(nameof(Endereco.EnderecoCompleto)).IsRequired();
});

        }
    }
}