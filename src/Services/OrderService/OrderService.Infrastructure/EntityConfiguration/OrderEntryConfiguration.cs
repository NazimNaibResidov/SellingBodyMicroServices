using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModel.OrderAggreage;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class OrderEntryConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           
            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
            });
            builder.Property<int>("orderStatusId")

                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();
            var navigator = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigator.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasOne(x => x.Buyer)
                .WithMany()
                .HasForeignKey(x => x.BuyerId);
            builder.HasOne(x => x.OrderStatus)
               .WithMany()
               .HasForeignKey("orderStatusId");
        }
    }
}