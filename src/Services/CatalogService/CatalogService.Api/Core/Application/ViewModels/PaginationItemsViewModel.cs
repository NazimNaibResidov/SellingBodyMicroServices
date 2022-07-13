using System.Collections.Generic;

namespace CatalogService.Api.Core.Application.ViewModels
{
    public class PaginationItemsViewModel<T> where T : class
    {
        public PaginationItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}