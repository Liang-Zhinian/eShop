namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;

    public class SitesContext : DbContext
    {
        public SitesContext(DbContextOptions<SitesContext> options) : base(options)
        {
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffLoginLocation> StaffLoginLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandingMap());
            modelBuilder.ApplyConfiguration(new SiteMap());

            modelBuilder.ApplyConfiguration(new LocationMap());
            modelBuilder.ApplyConfiguration(new LocationImageMap());

            modelBuilder.ApplyConfiguration(new StaffMap());
            modelBuilder.ApplyConfiguration(new StaffLoginLocationMap());
        }     
    }


	public class SitesContextDesignFactory : IDesignTimeDbContextFactory<SitesContext>
    {
        public SitesContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<SitesContext>()
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_SitesDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new SitesContext(optionsBuilder.Options);
        }
    }
}
