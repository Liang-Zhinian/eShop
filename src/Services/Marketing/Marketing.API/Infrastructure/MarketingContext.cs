namespace SaaSEqt.eShop.Services.Marketing.API.Infrastructure
{
    using EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using SaaSEqt.eShop.Services.Marketing.API.Model;

    public class MarketingContext : DbContext
    {
        public MarketingContext(DbContextOptions<MarketingContext> options) : base(options)
        {    
        }

        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CampaignEntityTypeConfiguration());
            builder.ApplyConfiguration(new RuleEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserLocationRuleEntityTypeConfiguration());
        }
    }

    public class MarketingContextDesignFactory : IDesignTimeDbContextFactory<MarketingContext>
    {
        public MarketingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarketingContext>()
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_MarketingDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new MarketingContext(optionsBuilder.Options);
        }
    }
}