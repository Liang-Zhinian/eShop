using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure.EntityConfigurations
{
    public class ServiceItemMap : IEntityTypeConfiguration<ServiceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.ServiceItemTable);

            builder.Property<Guid>("Id").HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>("Name").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("Description").IsRequired().HasColumnType(Constants.DbConstants.String2000);
            builder.Property<int>("DefaultTimeLength").IsRequired();
            builder.Property<bool>("AllowOnlineScheduling").IsRequired();
            builder.Property<Guid>("ServiceCategoryId").HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>("IndustryStandardCategoryName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("IndustryStandardSubcategoryName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<double>("TaxRate").IsRequired();
            builder.Property<double>("Price").IsRequired();
            //builder.Property<double>("TaxAmount").IsRequired();
            builder.Property<Guid>("SiteId").IsRequired().HasColumnType(Constants.DbConstants.KeyType);

            //builder.Ignore("Version");

            builder.HasOne(_ => _.ServiceCategory)
                   .WithMany()
                   .HasForeignKey(_ => _.ServiceCategoryId);


        }
    }
}
