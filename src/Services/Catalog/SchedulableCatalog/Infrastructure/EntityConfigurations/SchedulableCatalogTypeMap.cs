﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SaaSEqt.eShop.Services.SchedulableCatalog.Entities;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Infrastructure.EntityConfigurations
{
    public class SchedulableCatalogTypeMap : IEntityTypeConfiguration<SchedulableCatalogType>
    {
        public void Configure(EntityTypeBuilder<SchedulableCatalogType> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.ServiceCategoryTable);

            builder.Property<Guid>("Id").HasColumnType(Constants.DbConstants.KeyType);
            builder.Property<string>("Name").IsRequired().HasColumnType(Constants.DbConstants.String255);
            builder.Property<string>("Description").IsRequired().HasColumnType(Constants.DbConstants.String2000);
            builder.Property<bool>("AllowOnlineScheduling").IsRequired();
            builder.Property<int>("ScheduleTypeId").IsRequired();

            builder.HasOne(o => o.ScheduleType)
                    .WithMany()
                   .HasForeignKey("ScheduleTypeId");

        }
    }
}
