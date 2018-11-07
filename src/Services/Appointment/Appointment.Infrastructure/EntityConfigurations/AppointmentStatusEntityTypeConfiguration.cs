using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;
using SaaSEqt.eShop.Services.Appointment.Infrastructure;

namespace Appointment.Infrastructure.EntityConfigurations
{
    class AppointmentStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> appointmentStatusConfiguration)
        {
            appointmentStatusConfiguration.ToTable("AppointmentStatus");

            appointmentStatusConfiguration.HasKey(o => o.Id);

            appointmentStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            appointmentStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
