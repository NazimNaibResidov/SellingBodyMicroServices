using System.Collections.Generic;

namespace BasketService.Api.Core.Domain.Models
{
    public class CusomterBasket
    {
        public CusomterBasket(string cusmtomerId)
        {
            BuyerId = cusmtomerId;
        }

        public CusomterBasket()
        {
        }

        public string BuyerId { get; set; }
        public List<BaksetItem> Items = new List<BaksetItem>();
    }
}