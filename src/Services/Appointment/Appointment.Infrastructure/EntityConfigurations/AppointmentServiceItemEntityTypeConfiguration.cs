using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;
using SaaSEqt.eShop.Services.Appointment.Infrastructure;
using System;

namespace Appointment.Infrastructure.EntityConfigurations
{
    class AppointmentServiceItemEntityTypeConfiguration
        : IEntityTypeConfiguration<AppointmentServiceItem>
    {
        public void Configure(EntityTypeBuilder<AppointmentServiceItem> orderItemConfiguration)
        {
            orderItemConfiguration.ToTable("AppointmentServiceItem");

            orderItemConfiguration.HasKey(o => o.Id);

            orderItemConfiguration.Ignore(b => b.DomainEvents);

            orderItemConfiguration.Property(o => o.Id);

            orderItemConfiguration.Property<Guid>("AppointmentId")
                .IsRequired().HasColumnType("char(36)");

            orderItemConfiguration.Property<Guid>("ServiceItemId").HasColumnType("char(36)");
            orderItemConfiguration.Property<string>("Name").IsRequired().HasColumnType("varchar(255)");
            orderItemConfiguration.Property<string>("Description").IsRequired().HasColumnType("varchar(2000)");
            orderItemConfiguration.Property<int>("DefaultTimeLength").IsRequired();
            orderItemConfiguration.Property<bool>("AllowOnlineScheduling").IsRequired();
            orderItemConfiguration.Property<string>("IndustryStandardCategoryName").IsRequired().HasColumnType("varchar(255)");
            orderItemConfiguration.Property<string>("IndustryStandardSubcategoryName").IsRequired().HasColumnType("varchar(255)");
            orderItemConfiguration.Property<double>("TaxRate").IsRequired();
            orderItemConfiguration.Property<double>("Price").IsRequired();
            //builder.Property<double>("TaxAmount").IsRequired();
            orderItemConfiguration.Property<Guid>("SiteId").IsRequired().HasColumnType("char(36)");
        }
    }
}
