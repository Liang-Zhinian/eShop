using SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations.Constants;
using SaaSEqt.eShop.Services.Sites.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.EntityConfigurations
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(DbConstants.ClientTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.FirstName).IsRequired(false).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.LastName).IsRequired(true).HasColumnType(DbConstants.String255);
            builder.Property<int>("GenderId").IsRequired();
            builder.Property(_ => _.MobilePhone).IsRequired(true).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.HomePhone).IsRequired(false).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.WorkPhone).IsRequired(false).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.EmailAddress).IsRequired(false).HasColumnType(DbConstants.String255);
            builder.Property(_ => _.Birthdate).IsRequired(false);
            builder.Property(_ => _.DateOfFirstAppointment).IsRequired(false);
            builder.Property(_ => _.Notes).IsRequired(false).HasColumnType(DbConstants.String2000);
            builder.Property(_ => _.AvatarImage).IsRequired(false).HasColumnType(DbConstants.MediumBlob);

            builder.OwnsOne(c => c.Address, address => {
                address.Property("ClientId").HasColumnType(DbConstants.KeyType);
                address.Property("City").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("Country").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("ZipCode").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("StateProvince").IsRequired(false).HasColumnType(DbConstants.String255);
                address.Property("Street").IsRequired(false).HasColumnType(DbConstants.String255);
            });

        }
    }
}
