namespace Eva.eShop.Services.Catalog.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;
    using Eva.BuildingBlocks.IntegrationEventLogEF;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }     
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<CatalogContext>()
                .UseMySQL("Server=localhost;Database=Eva_eShop_Services_CatalogDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None;");

            return new CatalogContext(optionsBuilder.Options);
        }
    }

    public class IntegrationEventLogContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseMySql("Server=127.0.0.1;database=Eva_eShop_Services_CatalogDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}
