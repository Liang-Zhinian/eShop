namespace SaaSEqt.eShop.Services.Catalog.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;
    using SaaSEqt.eShop.Services.Catalog.API.Model.SchedulableCatalog;
    using SaaSEqt.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations.SchedulableCatalogs;

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        public DbSet<SchedulableCatalogType> SchedulableCatalogTypes { get; set; }
        public DbSet<SchedulableCatalogItem> SchedulableCatalogItems { get; set; }

        public DbSet<ScheduleType> ScheduleTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

            builder.ApplyConfiguration(new SchedulableCatalogItemMap());
            builder.ApplyConfiguration(new SchedulableCatalogTypeMap());

            builder.ApplyConfiguration(new ScheduleTypeMap());
            builder.ApplyConfiguration(new AvailabilityMap());
            builder.ApplyConfiguration(new UnavailabilityMap());
        }     
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<CatalogContext>()
                .UseMySql("Server=localhost;database=SaaSEqt.eShop.Services.CatalogDb;uid=book2;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
