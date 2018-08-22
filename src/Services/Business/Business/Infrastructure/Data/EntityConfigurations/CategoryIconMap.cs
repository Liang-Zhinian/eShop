using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Business.Domain.Model.Catalog;

namespace SaaSEqt.eShop.Business.Infrastructure.Data
{
    public class CategoryIconMap : IEntityTypeConfiguration<CategoryIcon>
    {
        public void Configure(EntityTypeBuilder<CategoryIcon> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.ServiceCategoryTable);

            builder.Property<int>("Id");
            builder.Property<string>("Name").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("IconUri").IsRequired().HasColumnType(Constants.DbConstants.String2000);
            builder.Property<string>("IconFileName").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<int>("ServiceCategoryId").IsRequired().HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>("Type").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<int>("Order");

            builder.HasOne(_ => _.ServiceCategory)
                   .WithMany()
                   .HasForeignKey(_ => _.ServiceCategoryId);
        }
    }
}
