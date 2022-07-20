using System.Collections.Generic;

namespace CatalogService.Api.Core.Domain
{
    public class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}