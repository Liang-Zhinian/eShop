using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Infra.Data.Mappings
{
    public class PersonMap : EntityWithCompositeIdMap<Person>, IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            Configure(builder, "Person");

            builder.OwnsOne(_ => _.Name, name => {
                //name.Property("PersonId").HasColumnType(Constants.DbConstants.KeyType);
                name.Property(_ => _.FirstName).HasColumnType(Constants.DbConstants.String255);
                name.Property(_ => _.LastName).HasColumnType(Constants.DbConstants.String255);
            });
            builder.OwnsOne(_ => _.ContactInformation, cb => {
                //cb.Property("PersonId").HasColumnType(Constants.DbConstants.KeyType);
                cb.OwnsOne(ci => ci.EmailAddress, em=>{
                    //em.Property("ContactInformationPersonId").HasColumnType(Constants.DbConstants.KeyType);
                    em.Property("Address").HasColumnType(Constants.DbConstants.String255);
                });
                cb.OwnsOne(ci => ci.PostalAddress, address => {
                    //address.Property("ContactInformationPersonId").HasColumnType(Constants.DbConstants.KeyType);
                    address.Property("City").HasColumnType(Constants.DbConstants.String255);
                    address.Property("CountryCode").HasColumnType(Constants.DbConstants.String255);
                    address.Property("PostalCode").HasColumnType(Constants.DbConstants.String255);
                    address.Property("StateProvince").HasColumnType(Constants.DbConstants.String255);
                    address.Property("StreetAddress").HasColumnType(Constants.DbConstants.String255);
                });
                cb.OwnsOne(ci => ci.PrimaryTelephone, phone => {
                    //phone.Property("ContactInformationPersonId").HasColumnType(Constants.DbConstants.KeyType);
                    phone.Property("Number").HasColumnType(Constants.DbConstants.String255);
                });
                cb.OwnsOne(ci => ci.SecondaryTelephone, phone => {
                    //phone.Property("ContactInformationPersonId").HasColumnType(Constants.DbConstants.KeyType);
                    phone.Property("Number").HasColumnType(Constants.DbConstants.String255);
                });
            });
            //builder.OwnsOne(_ => _.EmailAddress, em => {
            //    //em.Property("PersonId").HasColumnType(Constants.DbConstants.KeyType);
            //    em.Property("Address").HasColumnType(Constants.DbConstants.String255);
            //});
            builder.Ignore(_ => _.EmailAddress);
            builder.HasOne(_ => _.User)
                  .WithOne(_ => _.Person)
                  .HasForeignKey<Person>(_ => _.UserId);

            MapToTenant(builder);

            //builder.OwnsOne(y => y.TenantId);
        }
    }
}
