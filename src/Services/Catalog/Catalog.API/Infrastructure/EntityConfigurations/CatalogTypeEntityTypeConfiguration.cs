using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Catalog.API.Model;

namespace SaaSEqt.eShop.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CatalogTypeEntityTypeConfiguration
        : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                   .HasColumnType(Constants.DbConstants.KeyType)
                   .ValueGeneratedOnAdd()
               .IsRequired();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property<Guid>("MerchantId").HasColumnType(Constants.DbConstants.KeyType);
        }
    }
}
