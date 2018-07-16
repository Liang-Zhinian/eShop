using SaaSEqt.eShop.Services.SchedulableCatalog.Infrastructure.EntityConfigurations;
using SaaSEqt.eShop.Services.SchedulableCatalog.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.SchedulableCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Infrastructure
{
    public class SchedulableCatalogDbContext : DbContext
    {
        public DbSet<SchedulableCatalogType> ServiceCategories { get; set; }
        public DbSet<SchedulableCatalogItem> ServiceItems { get; set; }

        public DbSet<ScheduleType> ScheduleTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }
        
        public SchedulableCatalogDbContext(DbContextOptions<SchedulableCatalogDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new SchedulableCatalogItemMap());
            modelBuilder.ApplyConfiguration(new SchedulableCatalogTypeMap());

            modelBuilder.ApplyConfiguration(new ScheduleTypeMap());
            modelBuilder.ApplyConfiguration(new AvailabilityMap());
            modelBuilder.ApplyConfiguration(new UnavailabilityMap());

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

    public class SchedulableCatalogDbContextFactory : IDesignTimeDbContextFactory<SchedulableCatalogDbContext>
    {
        public SchedulableCatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchedulableCatalogDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=book2.schedulablecatalog;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new SchedulableCatalogDbContext(optionsBuilder.Options);
        }
    }
}
