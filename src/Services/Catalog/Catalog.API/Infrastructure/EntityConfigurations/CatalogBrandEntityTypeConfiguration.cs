using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Catalog.API.Model;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Eva.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CatalogBrandEntityTypeConfiguration
        : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseMySQLAutoIncrementColumn("catalog_brand_hilo")
               .IsRequired();

            builder.Property(cb => cb.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
