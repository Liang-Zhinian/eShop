using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eva.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Eva.eShop.Services.Ordering.Infrastructure;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class OrderItemEntityTypeConfiguration
        : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> orderItemConfiguration)
        {
            orderItemConfiguration.ToTable("orderItems"/*, OrderingContext.DEFAULT_SCHEMA*/);

            orderItemConfiguration.HasKey(o => o.Id);

            orderItemConfiguration.Ignore(b => b.DomainEvents);

            orderItemConfiguration.Property(o => o.Id)
                .UseMySQLAutoIncrementColumn("orderitemseq");

            orderItemConfiguration.Property<int>("OrderId")
                .IsRequired();

            orderItemConfiguration.Property<decimal>("Discount")
                .IsRequired();

            orderItemConfiguration.Property<int>("ProductId")
                .IsRequired();

            orderItemConfiguration.Property<string>("ProductName")
                .IsRequired();

            orderItemConfiguration.Property<decimal>("UnitPrice")
                .IsRequired();

            orderItemConfiguration.Property<int>("Units")
                .IsRequired();

            orderItemConfiguration.Property<string>("PictureUrl")
                .IsRequired(false);
        }
    }
}
