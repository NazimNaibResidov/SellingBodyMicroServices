using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastrcuture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastrcuture.EntityConfigration
{
    public class CatalogItemConfiugration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("CatalogItem", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseHiLo("Catalog_Item_Id")
                .IsRequired();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Ignore(x => x.PictureUrl)
              ;
            builder.Property(x => x.PictureFileName)
                .IsRequired(false);
            builder.HasOne(x => x.CatalogBrend)
                .WithMany(x=>x.CatalogItems)
                .HasForeignKey(x => x.CatalogBrendId);
            builder.HasOne(x => x.catalogType)
               .WithMany(x => x.CatalogItems)
               .HasForeignKey(x => x.CatalogTypeId);
        }
    }
}