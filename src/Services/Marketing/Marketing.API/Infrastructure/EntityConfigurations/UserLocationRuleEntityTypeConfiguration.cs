using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Marketing.API.Model;

namespace Eva.eShop.Services.Marketing.API.Infrastructure.EntityConfigurations
{
    class UserLocationRuleEntityTypeConfiguration
       : IEntityTypeConfiguration<UserLocationRule>
    {
        public void Configure(EntityTypeBuilder<UserLocationRule> builder)
        {
            builder.Property(r => r.LocationId)
            .HasColumnName("LocationId")
            .IsRequired();
        }
    }
}
