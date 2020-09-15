using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eva.eShop.Services.Identity.API.Models;
using Microsoft.EntityFrameworkCore.Design;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;

namespace Eva.eShop.Services.Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySQL("Server=localhost;Database=Eva_eShop_Services_IdentityDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

    public class ConfigurationDbContextDesignFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>()
                .UseMySQL("Server=localhost;Database=Eva_eShop_Services_IdentityDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None;");

            ConfigurationStoreOptions configurationStoreOptions = new ConfigurationStoreOptions();
            return new ConfigurationDbContext(optionsBuilder.Options, configurationStoreOptions);
        }
    }

    public class PersistedGrantDbContextDesignFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>()
                .UseMySQL("Server=localhost;Database=Eva_eShop_Services_IdentityDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None;");

            OperationalStoreOptions operationalStoreOptions = new OperationalStoreOptions();
            return new PersistedGrantDbContext(optionsBuilder.Options, operationalStoreOptions);
        }
    }

}
