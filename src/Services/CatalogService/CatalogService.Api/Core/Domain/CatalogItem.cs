﻿namespace CatalogService.Api.Core.Domain
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int CatalogTypeId { get; set; }
        public CatalogType catalogType { get; set; }

        public int CatalogBrendId { get; set; }
        public CatalogBrend CatalogBrend { get; set; }
    }
}