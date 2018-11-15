namespace SaaSEqt.eShop.Services.Catalog.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<ScheduleType> ScheduleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

            builder.ApplyConfiguration(new ServiceItemMap());
            builder.ApplyConfiguration(new ServiceCategoryMap());
            builder.ApplyConfiguration(new ScheduleTypeMap());
        }     
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<CatalogContext>()
                .UseMySql("Server=192.168.0.200;database=SaaSEqt_eShop_Services_CatalogDb;uid=book2;pwd=P@ssword;charset=utf8;port=33306;SslMode=None");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
