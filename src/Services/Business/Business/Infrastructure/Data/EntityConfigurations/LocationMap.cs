using SaaSEqt.eShop.Business.Infrastructure.Data.Constants;
using SaaSEqt.eShop.Business.Domain.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Business.Infrastructure.Data
{
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.LocationTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.Name).IsRequired().HasColumnType(DbConstants.String255);
            builder.Property(_ => _.Description).IsRequired(false).HasColumnType(DbConstants.String2000);
            builder.Property(_ => _.Image).IsRequired(false).HasColumnType(DbConstants.String1000);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);
            //builder.Property(_ => _.LocationAddressId).IsRequired(false).HasColumnType(DbConstants.KeyType);

            builder.OwnsOne(_ => _.ContactInformation, cb =>
            {
                cb.Property("LocationId").HasColumnType(DbConstants.KeyType);
                cb.Property<string>(e => e.ContactName).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.PrimaryTelephone).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.SecondaryTelephone).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.EmailAddress).HasColumnType(DbConstants.String255);
            });

            builder.OwnsOne(ci => ci.Address, address => {
                address.Property("LocationId").HasColumnType(DbConstants.KeyType);
                address.Property("City").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("Country").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("ZipCode").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("StateProvince").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("Street").IsRequired(false).HasColumnType(DbConstants.String255);
            });

            builder.OwnsOne(_ => _.Geolocation, cb =>
            {
                cb.Property("LocationId").HasColumnType(DbConstants.KeyType);
                cb.Property(e => e.Latitude);
                cb.Property(e => e.Longitude);
            });

            builder.HasOne(_ => _.Site)
                   .WithMany(_=>_.Locations)
                   .HasForeignKey(_=>_.SiteId)
                   .IsRequired();

        }
    }
}
