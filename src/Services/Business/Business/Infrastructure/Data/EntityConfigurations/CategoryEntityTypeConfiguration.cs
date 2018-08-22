using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Business.Domain.Model.Categories;

namespace SaaSEqt.eShop.Business.Infrastructure.Data
{
    class CategoryEntityTypeConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                   .UseMySQLAutoIncrementColumn("category_id")
               .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                   .HasMaxLength(255);
        }
    }
}
