using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Infra.Data.Mappings
{
    public class BaseMap<TEntity> where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder, string tableName)
        {
            builder.ForMySQLHasCharset("utf8");
            builder.ForMySQLHasCollation("utf8_general_ci");
            //builder.HasKey("Id");
            builder.ToTable(tableName);
            builder.Property<Guid>("Id")
                   .ValueGeneratedOnAdd()
                   .HasColumnType(Constants.DbConstants.KeyType)
                   .ForMySQLHasCollation("utf8_general_ci");

            //builder.Ignore(typeof(TenantId).FullName);
        }

        public virtual void MapToTenant(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .HasOne(typeof(Tenant).FullName, "Tenant")
                        .WithMany()
                   .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class EntityWithCompositeIdMap<TEntity> : BaseMap<TEntity> 
        where TEntity : EntityWithCompositeId
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder, string tableName)
        {
            builder.ForMySQLHasCharset("utf8");
            builder.ForMySQLHasCollation("utf8_general_ci");
            builder.HasKey(_ => _.Id);
            builder.ToTable(tableName);
            builder.Property(_=>_.Id)
                   .ValueGeneratedOnAdd()
                   //.HasColumnType(Constants.DbConstants.KeyType)
                   .ForMySQLHasCollation("utf8_general_ci");

            builder.Property<Guid>("TenantId")
                    .IsRequired()
                    .HasColumnType(Constants.DbConstants.KeyType)
                    .HasColumnName("TenantId")
                    .ForMySQLHasCollation("utf8_general_ci");

            //builder.Ignore(typeof(TenantId).FullName);

            //builder.OwnsOne(typeof(TenantId).FullName, "TenantId");
        }
    }
}
