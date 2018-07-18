using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure.EntityConfigurations
{
    public class UnavailabilityMap : IEntityTypeConfiguration<Unavailability>
    {
        public void Configure(EntityTypeBuilder<Unavailability> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.UnavailabilityTable);

            builder.Property<Guid>(_ => _.Id).HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>(_ => _.Description).IsRequired(false).HasColumnType(Constants.DbConstants.String2000);
            builder.Property<DateTime>(_ => _.StartDateTime).IsRequired();
            builder.Property<DateTime>(_ => _.EndDateTime).IsRequired();
            builder.Property<Guid>(_ => _.StaffId).IsRequired().HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<Guid>(_ => _.LocationId).IsRequired().HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<Guid>(_ => _.ServiceItemId).IsRequired().HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<bool>(_ => _.Sunday).IsRequired();
            builder.Property<bool>(_ => _.Monday).IsRequired();
            builder.Property<bool>(_ => _.Tuesday).IsRequired();
            builder.Property<bool>(_ => _.Wednesday).IsRequired();
            builder.Property<bool>(_ => _.Thursday).IsRequired();
            builder.Property<bool>(_ => _.Friday).IsRequired();
            builder.Property<bool>(_ => _.Saturday).IsRequired();
        }
    }
}
