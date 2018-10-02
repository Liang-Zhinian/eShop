using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Branding.API.Model;

namespace SaaSEqt.eShop.Services.Branding.API.Infrastructure.EntityConfigurations
{
    public class CategoryIconMap : IEntityTypeConfiguration<CategoryIcon>
    {
        public void Configure(EntityTypeBuilder<CategoryIcon> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable("CategoryIcon");

            builder.Property<int>("Id").UseMySQLAutoIncrementColumn("versionId");
            builder.Property<string>("Name").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("IconFileName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("SubcategoryName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("CategoryName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("Type").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<int>("Order");
            builder.Property<int>("VersionNumber");
            builder.Property<string>("Language").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<int>("Width");
            builder.Property<int>("Height");

            builder.Ignore(y => y.IconUri);
            //builder.Ignore(y => y.Width);
            //builder.Ignore(y => y.Height);
        }
    }
}
