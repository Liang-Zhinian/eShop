using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Services.Catalog.API.Model;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CatalogBrandEntityTypeConfiguration
        : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                   .HasColumnType(Constants.DbConstants.KeyType)
                   .ValueGeneratedOnAdd()
               .IsRequired();

            builder.Property(cb => cb.Brand)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property<Guid>("MerchantId").HasColumnType(Constants.DbConstants.KeyType);
        }
    }
}
