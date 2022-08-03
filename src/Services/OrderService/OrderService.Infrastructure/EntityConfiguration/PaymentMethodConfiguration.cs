using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using System;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.Property<int>("BuyerId").IsRequired();
             builder.Property(x=>x.CardType)
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("OrderStatusId")
              .IsRequired();
        }

    }
}