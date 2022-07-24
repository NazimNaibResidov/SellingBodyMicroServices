using CatalogService.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.Api.Infrastrcuture.Context
{
    public class CatalogContext : DbContext
    {
        public static string DEFAULT_SCHEMA = "catalog";

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<CatalogBrend> CatalogBrends { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}