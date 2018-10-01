namespace SaaSEqt.eShop.Services.Schedule.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;

    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Unavailability> Unavailabilities { get; set; }
        public DbSet<ScheduleType> ScheduleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AvailabilityMap());
            builder.ApplyConfiguration(new UnavailabilityMap());
            builder.ApplyConfiguration(new ScheduleTypeMap());
        }     
    }


	public class ScheduleContextDesignFactory : IDesignTimeDbContextFactory<ScheduleContext>
    {
        public ScheduleContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<ScheduleContext>()
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_ScheduleDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new ScheduleContext(optionsBuilder.Options);
        }
    }
}
