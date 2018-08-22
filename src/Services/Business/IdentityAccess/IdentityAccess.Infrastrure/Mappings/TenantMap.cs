using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants;

namespace SaaSEqt.IdentityAccess.Infrastructure.Mappings
{
    public class TenantMap : BaseMap<Tenant>,  IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder, DbConstants.TenantTable);
            
            //builder.HasKey(o => new { o.Id });
            //builder.ToTable(Constants.DbConstants.TenantTable);

            //builder.Property<Guid>("Id").HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>("Name").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("Description").HasColumnType(Constants.DbConstants.String2000);
            builder.Property<bool>("Active").IsRequired();

            //builder.OwnsOne(y => y.TenantId);

            //builder.HasAlternateKey("TenantId_Id").HasName("TenantId_Id");
            //OwnTenantId(builder);
            //builder.Property("TenantId_Id",
        }
    }
}
