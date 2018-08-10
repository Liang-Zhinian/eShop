namespace SaaSEqt.eShop.Services.IndustryStandardCategory.API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using EntityConfigurations;
    using Model;
    using Microsoft.EntityFrameworkCore.Design;

    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubcategoryEntityTypeConfiguration());
        }     
    }


    public class CategoryContextDesignFactory : IDesignTimeDbContextFactory<CategoryContext>
    {
        public CategoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<CategoryContext>()
                .UseMySql("Server=localhost;database=SaaSEqt_eShop_Services_CategoryDb;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None");

            return new CategoryContext(optionsBuilder.Options);
        }
    }
}
