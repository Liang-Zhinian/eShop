using SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Sites.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations
{
    public class StaffMap : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.StaffTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.IsMale).IsRequired();
            builder.Property(_ => _.IsEnabled).IsRequired();
            builder.Property(_ => _.Bio).HasColumnType(DbConstants.String2000);
            builder.Property(_ => _.Image).HasColumnType(DbConstants.String4000);
            builder.Property(_ => _.UserName).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.Password).HasColumnType(DbConstants.String2000);
            builder.Property(_ => _.FirstName).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.LastName).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.EmailAddress).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.City).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.CountryCode).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.PostalCode).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.StateProvince).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.StreetAddress).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.PrimaryTelephone).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.SecondaryTelephone).HasColumnType(DbConstants.String255);

            //builder.HasOne(_ => _.Site)
                   //.WithMany()
                   //.HasForeignKey(_ => _.SiteId);
        }
    }
}
