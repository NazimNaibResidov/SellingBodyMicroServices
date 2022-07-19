using CatalogService.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastrcuture.EntityConfigration
{
    public class CatalogTypeConfiugration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
