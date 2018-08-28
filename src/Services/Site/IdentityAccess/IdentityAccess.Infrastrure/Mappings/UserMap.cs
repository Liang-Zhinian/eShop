using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants;

namespace SaaSEqt.IdentityAccess.Infrastructure.Mappings
{
    public class UserMap : EntityWithCompositeIdMap<User>, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder, DbConstants.UserTable);

            builder.Property<string>("Username").HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("Password").HasColumnType(Constants.DbConstants.String255);
            builder.OwnsOne(_ => _.Enablement, et => {
                //et.Property("UserId").HasColumnType(Constants.DbConstants.KeyType);
                et.Property<bool>(_ => _.Enabled).IsRequired();
                et.Property<DateTime>("StartDate");
                et.Property<DateTime>("EndDate");
            });
            //builder.OwnsOne(_ => _.UserDescriptor, cb=>{
            //    cb.Property("UserId").HasColumnType(Constants.DbConstants.KeyType);
            //    cb.Property(y => y.TenantId).HasColumnType(Constants.DbConstants.KeyType);
            //    cb.Property(y => y.EmailAddress).HasColumnType(Constants.DbConstants.String255);
            //    cb.Property(y => y.Username).HasColumnType(Constants.DbConstants.String255);
            //    cb.ToTable("UserDescriptor");
            //});
            //builder.Ignore(_ => _.Person);
            //builder.OwnsOne(y => y.TenantId);

            MapToTenant(builder);
        }
    }
}
