using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Stash.Project.Stash.WarehouseManage.Model;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Stash.Project.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ProjectDbContext :
    AbpDbContext<ProjectDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    //实体上下文

    /// <summary>
    /// 单据类型表
    /// </summary>
    public DbSet<DocumentType> DocumentType { get; set; }
    /// <summary>
    /// 出库单表
    /// </summary>
    public DbSet<OutStorageTable> OutStorageTable { get; set; }
    /// <summary>
    /// 入库单状态表
    /// </summary>
    public DbSet<PutStorageStateTable> PutStorageStateTable { get; set; }
    /// <summary>
    /// 入库单表
    /// </summary>
    public DbSet<PutStorageTable> PutStorageTable { get; set; }
    /// <summary>
    /// 仓库产品关系表
    /// </summary>
    public DbSet<StashProductTable> StashProductTable { get; set; }

    #endregion

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
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
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ProjectConsts.DbTablePrefix + "YourEntities", ProjectConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});0

        //单据类型表
        builder.Entity<DocumentType>(x =>
        {
            x.ToTable("DocumentType");
            x.HasKey(y => y.Id);
            x.Property(y=>y.Document_Name).HasMaxLength(200).IsRequired();
        });

        //出库单表
        builder.Entity<OutStorageTable>(x =>
        {
            x.ToTable("OutStorageTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.OutStorageType_Id).IsRequired();
            x.Property(y => y.OutStorage_OrderId).IsRequired();
            x.Property(y => y.OuttStorage_SupplierId).IsRequired();
            x.Property(y => y.Operator_Date).IsRequired();
            x.Property(y => y.OutStorage_Name).HasMaxLength(200).IsRequired();
            x.Property(y => y.OutStorage_ContactPerson).HasMaxLength(200).IsRequired();
            x.Property(y => y.OutStorage_Phone).HasMaxLength(200).IsRequired();
            x.Property(y => y.Operator_Name).HasMaxLength(200).IsRequired();
            x.Property(y => y.OutStorage_Remark).HasMaxLength(200).IsRequired();          
        });

        //入库单状态表
        builder.Entity<PutStorageStateTable>(x =>
        {
            x.ToTable("PutStorageStateTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.PutStorageState_Name).HasMaxLength(200).IsRequired();
        });

        //入库单表
        builder.Entity<PutStorageTable>(x =>
        {
            x.ToTable("PutStorageTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.PutStorageType_Id).IsRequired();
            x.Property(y => y.PutStorage_OrderId).IsRequired();
            x.Property(y => y.PutStorage_SupplierId).IsRequired();
            x.Property(y => y.Operator_Date).IsRequired();
            x.Property(y => y.PutStorage_Name).HasMaxLength(200).IsRequired();
            x.Property(y => y.PutStorage_ContactPerson).HasMaxLength(200).IsRequired();
            x.Property(y => y.PutStorage_Phone).HasMaxLength(200).IsRequired();
            x.Property(y => y.Operator_Name).HasMaxLength(200).IsRequired();
            x.Property(y => y.PutStorage_Remark).HasMaxLength(200).IsRequired();
        });

        //仓库产品关系表
        builder.Entity<StashProductTable>(x =>
        {
            x.ToTable("StashProductTable");
            x.HasKey(y=>y.Id);
            x.Property(y => y.Product_Id).IsRequired();
            x.Property(y => y.PutStorage_Id).IsRequired();
            x.Property(y => y.PutStorage_Lot).IsRequired();
            x.Property(y => y.PutStorage_Price).HasColumnType("decimal(32,2)").IsRequired();
            x.Property(y => y.PutStorage_Num).IsRequired();
            x.Property(y => y.PutStorage_SumPrice).HasColumnType("decimal(32,2)").IsRequired();
            x.Property(y => y.PutStorage_Position).HasMaxLength(200).IsRequired();
        });
    }
}
