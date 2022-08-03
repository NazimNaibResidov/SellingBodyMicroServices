using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfiguration
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public  void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable(nameof(builder), OrderDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Ignore(i => i.DomainEvents);
            builder.Property(x => x.CreateDate).ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.HasMany(x => x.PaymentMethods)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            var navigator = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));
            navigator.SetPropertyAccessMode(PropertyAccessMode.Field);
            
        }
    }
}