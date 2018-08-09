using SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Services.Business.Infrastructure.EntityConfigurations
{
    public class SiteMap : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.SiteTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.Name).IsRequired().HasColumnType(DbConstants.String255);
            builder.Property(_ => _.Description).HasColumnType(DbConstants.String2000);
            builder.Property(_ => _.Active).IsRequired();
            builder.Property(_ => _.TenantId).HasColumnType(DbConstants.KeyType);

            builder.OwnsOne(_ => _.ContactInformation, cb => {
                cb.Property("SiteId").HasColumnType(DbConstants.KeyType);
                cb.Property<string>(e => e.ContactName).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.PrimaryTelephone).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.SecondaryTelephone).HasColumnType(DbConstants.String255);
                cb.Property<string>(e => e.EmailAddress).HasColumnType(DbConstants.String255);
            });
        }
    }
}
