using System.Collections.Generic;

namespace CatalogService.Api.Core.Domain
{
    public class CatalogBrend
    {
        public int Id { get; set; }
        public string Brend { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}