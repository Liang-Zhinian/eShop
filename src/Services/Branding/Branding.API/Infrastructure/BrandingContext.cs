namespace SaaSEqt.eShop.Services.Branding.API.Infrastructure
{
    using EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Model;

    public class BrandingContext : DbContext
    {
        public BrandingContext(DbContextOptions<BrandingContext> options) : base(options)
        {
        }

        public DbSet<Version> Versions { get; set; }
        public DbSet<CategoryIcon> CategoryIcons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new VersionMap());
            builder.ApplyConfiguration(new CategoryIconMap());
        }     
    }


    public class BrandingContextDesignFactory : IDesignTimeDbContextFactory<BrandingContext>
    {
        public BrandingContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<BrandingContext>()
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_BrandingDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new BrandingContext(optionsBuilder.Options);
        }
    }
}
