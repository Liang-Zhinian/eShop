using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Business.Domain.Model.Catalog;

namespace SaaSEqt.eShop.Business.Infrastructure.Data
{
    public class AvailabilityMap : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.AvailabilityTable);

            builder.Property<Guid>(_ => _.Id).HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<DateTime>(_=>_.StartDateTime).IsRequired();
            builder.Property<DateTime>(_ => _.EndDateTime).IsRequired();
            builder.Property<DateTime>(_ => _.BookableEndDateTime);
            builder.Property<Guid>(_=>_.StaffId).IsRequired().HasColumnType(Constants.DbConstants.KeyType);
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
