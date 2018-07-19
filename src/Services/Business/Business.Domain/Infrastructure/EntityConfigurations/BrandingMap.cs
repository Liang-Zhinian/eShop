using SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations
{
    public class BrandingMap : IEntityTypeConfiguration<Branding>
    {
        public void Configure(EntityTypeBuilder<Branding> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.BrandingTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.Logo).HasColumnType(DbConstants.String4000);
            builder.Property(_ => _.PageColor1).HasColumnType(DbConstants.String10);
            builder.Property(_ => _.PageColor1).HasColumnType(DbConstants.String10);
            builder.Property(_ => _.PageColor2).HasColumnType(DbConstants.String10);
            builder.Property(_ => _.PageColor3).HasColumnType(DbConstants.String10);
            builder.Property(_ => _.PageColor4).HasColumnType(DbConstants.String10);

            builder.HasOne(_ => _.Site)
                   .WithOne(_ => _.Branding)
                   .HasForeignKey<Branding>(_ => _.SiteId);
        }
    }
}
