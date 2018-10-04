using SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Sites.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations
{
    public class LocationImageMap : IEntityTypeConfiguration<LocationImage>
    {
        public void Configure(EntityTypeBuilder<LocationImage> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.LocationImageTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.LocationId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.Image).HasColumnType(DbConstants.String1000);
            builder.Ignore(y => y.ImageUri);

            builder.HasOne(_ => _.Site)
                   .WithMany()
                   .HasForeignKey(_ => _.SiteId);

            builder.HasOne(p => p.Location)
                   .WithMany(p => p.AdditionalLocationImages)
                   .HasForeignKey(f => f.LocationId);

            /*
             * builder.HasOne(typeof(Site).FullName)
                   .WithMany()
                   .HasForeignKey("SiteId");

            builder.HasOne(typeof(Location).FullName)
                   .WithMany()
                   .HasForeignKey("LocationId");
            */
        }
    }
}
