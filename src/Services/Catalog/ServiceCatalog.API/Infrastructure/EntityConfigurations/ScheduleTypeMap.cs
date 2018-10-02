using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure.EntityConfigurations
{
    public class ScheduleTypeMap : IEntityTypeConfiguration<ScheduleType>
    {
        public void Configure(EntityTypeBuilder<ScheduleType> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable(Constants.DbConstants.ScheduleTypeTable);

            builder.Property<int>("Id")
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property<string>("Name").IsRequired()
                   .HasColumnType(Constants.DbConstants.String255);
        }
    }
}
