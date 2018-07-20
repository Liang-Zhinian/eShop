using SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations;
using SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SaaSEqt.eShop.Services.Business.Infrastructure
{
    public class BusinessDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(DbConstants.Schema);

            modelBuilder.ApplyConfiguration(new BrandingMap());
            modelBuilder.ApplyConfiguration(new SiteMap());

            modelBuilder.ApplyConfiguration(new LocationMap());
            modelBuilder.ApplyConfiguration(new LocationImageMap());

            modelBuilder.ApplyConfiguration(new StaffMap());
            modelBuilder.ApplyConfiguration(new StaffLoginLocationMap());

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

    public class BusinessDbContextFactory : IDesignTimeDbContextFactory<BusinessDbContext>
    {
        public BusinessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_BusinessDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new BusinessDbContext(optionsBuilder.Options);
        }
    }
}
