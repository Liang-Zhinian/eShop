using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Infra.Data.Mappings.Constants;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Infra.Data.Mappings
{
    public class GroupMemberMap :BaseMap<GroupMember>,  IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            base.Configure(builder, DbConstants.GroupMemberTable);

            builder.Property<string>(_ => _.Name).IsRequired().HasColumnType(DbConstants.String255);
            builder.Property<Guid>("GroupId").IsRequired(); //.HasColumnType(DbConstants.KeyType);
            builder.Property<int>(_=>_.TypeValue).IsRequired().HasColumnName("Type");

            MapToTenant(builder);

            builder
                .HasOne(typeof(Group))
                .WithMany("GroupMembers")
                .HasForeignKey("GroupId");

            //builder.OwnsOne(y => y.TenantId);
        }
    }
}
