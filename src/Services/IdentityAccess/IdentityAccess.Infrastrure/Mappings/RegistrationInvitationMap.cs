using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Infrastructure.Mappings
{
    public class RegistrationInvitationMap : BaseMap<RegistrationInvitation>, IEntityTypeConfiguration<RegistrationInvitation>
    {
        public void Configure(EntityTypeBuilder<RegistrationInvitation> builder)
        {
            base.Configure(builder, DbConstants.RegistrationInvitationTable);

            builder.Property<string>(_ => _.Description).HasColumnType(Constants.DbConstants.String2000);
            builder.Property<string>(_ => _.InvitationId).IsRequired().HasColumnType(Constants.DbConstants.String36);
            builder.Property<DateTime>(_ => _.StartingOn).IsRequired();
            builder.Property<Guid>(_ => _.TenantId).IsRequired(); //.HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<DateTime>(_ => _.Until).IsRequired();

            MapToTenant(builder);

        }
    }
}
