using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Infrastructure.Mappings
{
    public class GroupMap : EntityWithCompositeIdMap<Group>, IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            base.Configure(builder, DbConstants.GroupTable);

            builder.Property<string>(_ => _.Name).IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>(_ => _.Description).HasColumnType(Constants.DbConstants.String2000);

            MapToTenant(builder);

            //builder.OwnsOne(y => y.TenantId);
        }
    }
}
