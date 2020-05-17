using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Marketing.API.Model;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Eva.eShop.Services.Marketing.API.Infrastructure.EntityConfigurations
{
    class RuleEntityTypeConfiguration
       : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("Rule");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
               .UseMySQLAutoIncrementColumn("rule_hilo")
               .IsRequired();

            builder.HasDiscriminator<int>("RuleTypeId")
                .HasValue<UserProfileRule>(RuleType.UserProfileRule.Id)
                .HasValue<PurchaseHistoryRule>(RuleType.PurchaseHistoryRule.Id)
                .HasValue<UserLocationRule>(RuleType.UserLocationRule.Id);

            builder.Property(r => r.Description)
                .HasColumnName("Description")
                .IsRequired();
        }
    }
}
