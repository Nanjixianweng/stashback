using Microsoft.EntityFrameworkCore;
using Stash.Project.Stash.BusinessManage.Model;
using Stash.Project.Stash.SystemSetting.Model;
using Stash.Project.Stash.WarehouseManage.Model;
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
    #region 业务
    /// <summary>
    /// 采购表
    /// </summary>
    public DbSet<PurchaseTable> PurchaseTable { get; set; }
    /// <summary>
    /// 采购产品关系表
    /// </summary>
    public DbSet<PurchaseProductRelationshipTable> PurchaseProductRelationshipTable { get; set; }
    /// <summary>
    /// 采购退货表
    /// </summary>
    public DbSet<PurchaseReturnGoodsTable> PurchaseReturnGoodsTable { get; set; }
    /// <summary>
    /// 销售表
    /// </summary>
    public DbSet<SellTable> SellTable { get; set; }
    /// <summary>
    /// 销售产品关系表
    /// </summary>
    public DbSet<SellProductRelationshipTable> SellProductRelationshipTable { get; set; }
    /// <summary>
    /// 销售退货表
    /// </summary>
    public DbSet<SalesReturnsTable> SalesReturnsTable { get; set; }
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

        //系统设置
        //用户
        builder.Entity<UserInfo>(b =>
        {
            b.ToTable("UserInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });
        //权限
        builder.Entity<AccessInfo>(b =>
        {
            b.ToTable("AccessInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });
        //角色
        builder.Entity<RoleInfo>(b =>
        {
            b.ToTable("RoleInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });
        //部门
        builder.Entity<SectorInfo>(b =>
        {
            b.ToTable("SectorInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });
        //角色用户
        builder.Entity<RoleUserInfo>(b =>
        {
            b.ToTable("RoleUserInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });
        //角色权限
        builder.Entity<RoleAccessInfo>(b =>
        {
            b.ToTable("RoleAccessInfo");
            b.HasKey(y => y.Id);
            b.Property(u => u.Id).IsRequired().HasMaxLength(50);
        });


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
        #region 业务
        //采购表
        builder.Entity<PurchaseTable>(x =>
        {
            x.ToTable("PurchaseTable");
            x.HasKey(y=>y.Id);
            x.Property(y => y.AssociatedOrderNumber).HasMaxLength(50).IsRequired();
            x.Property(y => y.SupplierName).HasMaxLength(50).IsRequired();
            x.Property(y => y.CustomerName).HasMaxLength(50).IsRequired();
            x.Property(y => y.ContactPerson).HasMaxLength(50).IsRequired();
            x.Property(y => y.Telephone).HasMaxLength(50).IsRequired();
            x.Property(y => y.Remark).HasMaxLength(50).IsRequired();
            x.Property(y => y.Documenter).HasMaxLength(50).IsRequired();
        });
        //采购产品关系
        builder.Entity<PurchaseProductRelationshipTable>(x =>
        {
            x.ToTable("PurchaseProductRelationshipTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.TotalPrice).HasColumnType("decimal(32,2)").IsRequired();
        });
        //采购退货表
        builder.Entity<PurchaseReturnGoodsTable>(x =>
        {
            x.ToTable("PurchaseReturnGoodsTable");
            x.HasKey(y => y.Id);
        });
        //销售表
        builder.Entity<SellTable>(x =>
        {
            x.ToTable("SellTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.AssociatedOrderNumber).HasMaxLength(50).IsRequired();
            x.Property(y => y.SupplierName).HasMaxLength(50).IsRequired();
            x.Property(y => y.CustomerName).HasMaxLength(50).IsRequired();
            x.Property(y => y.ContactPerson).HasMaxLength(50).IsRequired();
            x.Property(y => y.Telephone).HasMaxLength(50).IsRequired();
            x.Property(y => y.Remark).HasMaxLength(50).IsRequired();
            x.Property(y => y.Documenter).HasMaxLength(50).IsRequired();
        });
        //销售产品关系
        builder.Entity<SellProductRelationshipTable>(x =>
        {
            x.ToTable("SellProductRelationshipTable");
            x.HasKey(y => y.Id);
            x.Property(y => y.TotalPrice).HasColumnType("decimal(32,2)").IsRequired();
        });
        //销售退货表
        builder.Entity<SalesReturnsTable>(x =>
        {
            x.ToTable("SalesReturnsTable");
            x.HasKey(y => y.Id);
        });
        #endregion
    }
}
