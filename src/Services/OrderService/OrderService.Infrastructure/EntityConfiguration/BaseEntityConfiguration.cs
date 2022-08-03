using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfiguration
{
    //public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    //{
    //    public virtual void Configure(EntityTypeBuilder<T> builder)
    //    {
    //        //builder.ToTable(nameof(T), OrderDbContext.DEFAULT_SCHEMA);
    //        //builder.HasKey(x => x.Id);
    //        //builder.Property(x => x.Id).ValueGeneratedOnAdd();
    //        //builder.Ignore(i => i.DomainEvents);
    //        //builder.Property(x => x.CreateDate).ValueGeneratedOnAdd();
            
    //    }
    //}
}