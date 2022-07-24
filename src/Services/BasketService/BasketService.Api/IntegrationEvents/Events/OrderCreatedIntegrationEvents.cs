using BasketService.Api.Core.Domain.Models;
using EventBus.Base.Events;
using System;

namespace BasketService.Api.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvents : IntegrationEvent
    {
        public OrderCreatedIntegrationEvents(string userId, string userName, int orderNumber, string city, string streat, string state, string country, string zipCode, string cartNumber, string cartHolderName, DateTime cartExpriration, string cartSecurityNumber, int cartTypeId, string buyer, Guid requestId, CusomterBasket cusomterBasket)
        {
            UserId = userId;
            UserName = userName;
            OrderNumber = orderNumber;
            City = city;
            Streat = streat;
            State = state;
            Country = country;
            ZipCode = zipCode;
            CartNumber = cartNumber;
            CartHolderName = cartHolderName;
            CartExpriration = cartExpriration;
            CartSecurityNumber = cartSecurityNumber;
            CartTypeId = cartTypeId;
            Buyer = buyer;
            RequestId = requestId;
            CusomterBasket = cusomterBasket;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
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
        public CusomterBasket CusomterBasket { get; set; }
    }
}