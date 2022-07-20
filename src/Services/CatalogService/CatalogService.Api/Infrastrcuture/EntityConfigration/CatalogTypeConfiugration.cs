using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastrcuture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastrcuture.EntityConfigration
{
    public class CatalogTypeConfiugration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseHiLo("Catalog_Type_Id")
                .IsRequired();
            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}