using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Branding.API.Model;

namespace SaaSEqt.eShop.Services.Branding.API.Infrastructure.EntityConfigurations
{
    public class VersionMap : IEntityTypeConfiguration<Model.Version>
    {
        public void Configure(EntityTypeBuilder<Model.Version> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable("Version");

            builder.Property<int>("Id").UseMySQLAutoIncrementColumn("versionId");
            builder.Property<string>("Type").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<int>("VersionNumber");
            builder.Property<string>("Language").IsRequired().HasColumnType(Constants.DbConstants.String255);

        }
    }
}
