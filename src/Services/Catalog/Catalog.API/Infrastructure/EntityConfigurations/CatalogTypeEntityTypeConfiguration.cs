using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Catalog.API.Model;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Eva.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CatalogTypeEntityTypeConfiguration
        : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseMySQLAutoIncrementColumn("catalog_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
