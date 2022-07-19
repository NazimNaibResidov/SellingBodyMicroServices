using CatalogService.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastrcuture.EntityConfigration
{
    public class CatalogBrendConfiugration : IEntityTypeConfiguration<CatalogBrend>
    {
        public void Configure(EntityTypeBuilder<CatalogBrend> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
