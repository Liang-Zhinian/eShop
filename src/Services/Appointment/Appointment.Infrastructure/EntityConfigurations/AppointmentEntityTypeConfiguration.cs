using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate;
using AppointmentAggregate = SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;
using SaaSEqt.eShop.Services.Appointment.Infrastructure;
using System;

namespace Appointment.Infrastructure.EntityConfigurations
{
    class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentAggregate.Appointment>
    {
        public void Configure(EntityTypeBuilder<AppointmentAggregate.Appointment> orderConfiguration)
        {
            orderConfiguration.ToTable("Appointment"/*, OrderingContext.DEFAULT_SCHEMA*/);

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Ignore(b => b.DomainEvents);

            orderConfiguration.Property(o => o.Id);
            
            orderConfiguration.Property<Guid>("AppointmentId").IsRequired();
            orderConfiguration.Property<Guid>("StaffId").IsRequired();
            orderConfiguration.Property<Guid>("LocationId").IsRequired();
            orderConfiguration.Property<Guid>("SiteId").IsRequired();
            orderConfiguration.Property<Guid>("ClientId").IsRequired();
            orderConfiguration.Property<bool>("StaffRequested").IsRequired();
            orderConfiguration.Property<bool>("FirstAppointment").IsRequired();
            orderConfiguration.Property<DateTime>("OrderDate").IsRequired();
            orderConfiguration.Property<DateTime>("StartDateTime").IsRequired();
            orderConfiguration.Property<DateTime>("EndDateTime").IsRequired();
            orderConfiguration.Property<int>("Duration").IsRequired();
            orderConfiguration.Property<int>("AppointmentStatusId").IsRequired();
            orderConfiguration.Property<Guid?>("PaymentMethodId").IsRequired(false);
            orderConfiguration.Property<string>("Notes").IsRequired(false);

            var navigation = orderConfiguration.Metadata.FindNavigation(nameof(AppointmentAggregate.Appointment.AppointmentServiceItems));
            
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            orderConfiguration.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("PaymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            //orderConfiguration.HasOne<Buyer>()
            //    .WithMany()
            //    .IsRequired(false)
            //    .HasForeignKey("BuyerId");

            orderConfiguration.HasOne(o => o.AppointmentStatus)
                .WithMany()
                .HasForeignKey("AppointmentStatusId");
        }
    }
}
