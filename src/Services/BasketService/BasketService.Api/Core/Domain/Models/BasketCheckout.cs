using System;

namespace BasketService.Api.Core.Domain.Models
{
    public class BasketCheckout
    {
        public string City { get; set; }
        public string Streat { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CartNumber { get; set; }
        public string CartHolderName { get; set; }
        public DateTime CartExpriration { get; set; }
        public string CartSecurityNumber { get; set; }
        public int CartTypeId { get; set; }
        public string Buyer { get; set; }
        public Guid RequestId { get; set; }
    }
}