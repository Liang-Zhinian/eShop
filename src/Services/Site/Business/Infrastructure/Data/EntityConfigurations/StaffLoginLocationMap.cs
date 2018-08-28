using SaaSEqt.eShop.Business.Infrastructure.Data.Constants;
using SaaSEqt.eShop.Business.Domain.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaaSEqt.eShop.Business.Infrastructure.Data
{
    public class StaffLoginLocationMap : IEntityTypeConfiguration<StaffLoginLocation>
    {
        public void Configure(EntityTypeBuilder<StaffLoginLocation> builder)
        {
            builder
                .HasKey(t => new { t.Id, t.StaffId, t.LocationId });
            builder.ToTable(DbConstants.StaffLoginLocationTable);

            builder.Property(_ => _.Id).HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.LocationId).IsRequired().HasColumnType(DbConstants.KeyType);
            builder.Property(_ => _.SiteId).IsRequired().HasColumnType(DbConstants.KeyType);


            builder.HasOne(_ => _.Site)
                   .WithMany()
                   .HasForeignKey(_ => _.SiteId);
            
            builder
                .HasOne(sl => sl.Staff)
                .WithMany(p => p.StaffLoginLocations)
                .HasForeignKey(sl => sl.StaffId);
            
            //builder
                //.HasOne(sl => sl.Location)
                //.WithMany(p => p.StaffLoginLocations)
                //.HasForeignKey(sl => sl.LocationId);
        }
    }
}
