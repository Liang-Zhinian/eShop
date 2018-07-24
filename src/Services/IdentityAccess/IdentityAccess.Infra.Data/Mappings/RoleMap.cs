using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Domain.Access.Entities;
using SaaSEqt.IdentityAccess.Infra.Data.Mappings.Constants;

namespace SaaSEqt.IdentityAccess.Infra.Data.Mappings
{
    public class RoleMap : EntityWithCompositeIdMap<Role>, IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder, DbConstants.RoleTable);

            builder.Property<Guid>("GroupId").IsRequired(); //.HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>(_=>_.Name).IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>(_ => _.Description).HasColumnType(Constants.DbConstants.String2000);
            builder.Property<bool>(_ => _.SupportsNesting).IsRequired();

            MapToTenant(builder);
            /* not worked
            builder
                .HasOne(typeof(Group), "GroupId")
                .WithMany()
                .HasForeignKey("GroupId"); */
            /* worked */
            builder
                .HasOne(_ => _.Group)
                .WithMany()
                .HasForeignKey(_ => _.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
