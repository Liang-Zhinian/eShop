using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Catalog.API.Model;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Eva.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CatalogMediaEntityTypeConfiguration
        : IEntityTypeConfiguration<CatalogMedia>
    {
        public void Configure(EntityTypeBuilder<CatalogMedia> builder)
        {
            builder.ToTable("CatalogMedia");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseMySQLAutoIncrementColumn("catalog_media_hilo")
               .IsRequired();

            builder.Property(ci => ci.MediaFileName)
                .IsRequired(false);

            builder.Ignore(ci => ci.MediaUri);

            builder.HasOne(ci => ci.CatalogItem)
                .WithMany(ci => ci.CatalogMedias)
                .HasForeignKey(ci => ci.CatalogItemId);
        }
    }
}
