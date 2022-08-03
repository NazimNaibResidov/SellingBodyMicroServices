using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.ToTable("cardTypes", OrderDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(x => x.Name)
                  .HasMaxLength(200)
                  .IsRequired();
        }
    }
}