using System.Collections.Generic;

namespace OrderService.Domain.Models
{
    public class CusomterBasket
    {
        public CusomterBasket(string buyerId)
        {
            BuyerId = buyerId;
            items = new List<BasketItem>();
        }

        public CusomterBasket()
        {
        }

        public string BuyerId { get; set; }
        public List<BasketItem> items { get; set; }
    }
}