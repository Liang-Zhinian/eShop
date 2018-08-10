namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure
{
    using EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Model;

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        //public DbSet<CategoryIcon> CategoryIcons { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        public DbSet<ScheduleType> ScheduleTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.ApplyConfiguration(new CategoryIconMap());

            builder.ApplyConfiguration(new ServiceItemMap());
            builder.ApplyConfiguration(new ServiceCategoryMap());

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
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_ServiceCatalogDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
