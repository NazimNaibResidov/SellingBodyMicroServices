using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModel.OrderAggreage;
using OrderService.Infrastructure.Context;
using System;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem), OrderDbContext.DEFAULT_SCHEMA);
            //builder.HasKey(x => x.ProductId);
            //builder.Property(x => x.ProductId).ValueGeneratedOnAdd();
           
            builder.Property<int>("OrderId").IsRequired();
        }
    }
}