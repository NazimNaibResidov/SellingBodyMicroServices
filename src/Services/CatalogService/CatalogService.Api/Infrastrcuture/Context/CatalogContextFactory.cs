using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CatalogService.Api.Infrastrcuture.Context
{
    public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
            optionsBuilder.UseSqlServer("WINDOWS-N3J379M\\SQLEXPRESS01;database=catalog;trusted_connection=true;");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}