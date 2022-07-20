using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastrcuture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastrcuture.EntityConfigration
{
    public class CatalogBrendConfiugration : IEntityTypeConfiguration<CatalogBrend>
    {
        public void Configure(EntityTypeBuilder<CatalogBrend> builder)
        {
            builder.ToTable("CatalogBrend", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseHiLo("Catalog_Brend_Id")
                .IsRequired();
            builder.Property(x => x.Brend)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}