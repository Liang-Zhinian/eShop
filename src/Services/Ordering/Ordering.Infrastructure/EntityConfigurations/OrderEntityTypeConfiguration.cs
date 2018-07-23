using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore.Extensions;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using SaaSEqt.eShop.Services.Ordering.Infrastructure;
using System;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("orders"/*, OrderingContext.DEFAULT_SCHEMA*/);

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Ignore(b => b.DomainEvents);

            orderConfiguration.Property(o => o.Id)
                              .UseMySQLAutoIncrementColumn("orderseq"/*, OrderingContext.DEFAULT_SCHEMA*/);

            //Address value object persisted as owned entity type supported since EF Core 2.0
            orderConfiguration.OwnsOne(o => o.Address, y=>{
                y.Property(_ => _.Street);
                y.Property(_ => _.City);
                y.Property(_ => _.State);
                y.Property(_ => _.Country);
                y.Property(_ => _.ZipCode);
            });

            orderConfiguration.Property<DateTime>("OrderDate").IsRequired();
            orderConfiguration.Property<int?>("BuyerId").IsRequired(false);
            orderConfiguration.Property<int>("OrderStatusId").IsRequired();
            orderConfiguration.Property<int?>("PaymentMethodId").IsRequired(false);
            orderConfiguration.Property<string>("Description").IsRequired(false);

            var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Order.OrderItems));
            
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            orderConfiguration.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("PaymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            orderConfiguration.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("BuyerId");

            orderConfiguration.HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey("OrderStatusId");
        }
    }
}
