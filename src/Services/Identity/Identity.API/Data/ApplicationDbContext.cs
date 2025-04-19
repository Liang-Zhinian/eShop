namespace Eva.eShop.Services.Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<ExternalAccounts> ExternalAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<ExternalAccounts>()
            //       .HasOne(y => y.User)
            //       .WithOne(y => y.ExternalAccounts)
            //       .HasForeignKey<ExternalAccounts>(y => y.UserId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
