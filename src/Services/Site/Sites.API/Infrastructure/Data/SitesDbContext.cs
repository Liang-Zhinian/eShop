using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data.Constants;
using SaaSEqt.eShop.Services.Sites.API.Model.Security;
using SaaSEqt.eShop.Services.Sites.API.Model.Catalog;
using SaaSEqt.eShop.Services.Sites.API.Model.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data
{
    public class SitesDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        public DbSet<ScheduleType> ScheduleTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        public SitesDbContext(DbContextOptions<SitesDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantMap());

            modelBuilder.ApplyConfiguration(new BrandingMap());
            modelBuilder.ApplyConfiguration(new SiteMap());

            modelBuilder.ApplyConfiguration(new LocationMap());
            modelBuilder.ApplyConfiguration(new LocationImageMap());

            modelBuilder.ApplyConfiguration(new StaffMap());
            modelBuilder.ApplyConfiguration(new StaffLoginLocationMap());


            modelBuilder.ApplyConfiguration(new ServiceItemMap());
            modelBuilder.ApplyConfiguration(new ServiceCategoryMap());

            modelBuilder.ApplyConfiguration(new ScheduleTypeMap());
            modelBuilder.ApplyConfiguration(new AvailabilityMap());
            modelBuilder.ApplyConfiguration(new UnavailabilityMap());

            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoryEntityTypeConfiguration());

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    modelBuilder.Entity(entityType.Name).Property<DateTime>
            //      ("LastModified");
            //    //modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            //}
        }

        //public override int SaveChanges()
        //{
        //    foreach (var entry in ChangeTracker.Entries()
        //      .Where(e => e.State == EntityState.Added ||
        //       e.State == EntityState.Modified))
        //    {
        //        if (!(entry.Entity is ContactInformation) &&
        //            !(entry.Entity is PostalAddress) &&
        //            !(entry.Entity is DateTimeSlot) &&
        //            !(entry.Entity is Geolocation) &&
        //            !(entry.Entity is Gender))
        //            entry.Property("LastModified").CurrentValue = DateTime.Now;
        //    }
        //    return base.SaveChanges();
        //}
    }

    public class BusinessDbContextFactory : IDesignTimeDbContextFactory<SitesDbContext>
    {
        public SitesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SitesDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=Book2_BusinessDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new SitesDbContext(optionsBuilder.Options);
        }
    }
}
