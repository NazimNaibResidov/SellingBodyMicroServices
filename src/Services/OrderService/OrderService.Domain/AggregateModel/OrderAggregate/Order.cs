using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.Events;
using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Domain.AggregateModel.OrderAggreage
{
    public class Order
    : BaseEntity, IAggregateRoot
    {
       
        private DateTime OrderDate;
        public int Quantity { get;private set; }
        public string Description { get; private set; }
        public Guid? BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public Address Address { get; private set; }

        private int orderStatusId;

        public OrderStatus OrderStatus { get; private set; }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        private Guid? PaymentMethodId;
       
       
        //private bool _isDraft;

     
        private readonly List<OrderItem> _orderItems;

       

       

        public static Order NewDraft()
        {
            var order = new Order();
          
            return order;
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
            Id = Guid.NewGuid();
        }

        public Order(Guid userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
                string cardHolderName, DateTime cardExpiration, Guid? buyerId = null, Guid? paymentMethodId = null) : this()
        {
            BuyerId = buyerId;
            PaymentMethodId = paymentMethodId;
            orderStatusId = OrderStatus.Submitted.Id;
            OrderDate = DateTime.UtcNow;
            Address = address;

            AddOrderStartedDomainEvent(userId, userName, cardTypeId, cardNumber,
                                        cardSecurityNumber, cardHolderName, cardExpiration);
        }

       public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            var orderItems = new OrderItem(productName, pictureUrl, unitPrice, discount, units, productId);
            _orderItems.Add(orderItems);
        }

        public void SetPaymentId(Guid id)
        {
            PaymentMethodId = id;
        }

        public void SetBuyerId(Guid id)
        {
            BuyerId = id;
        }

        public void SetAwaitingValidationStatus()
        {
            if (orderStatusId == OrderStatus.Submitted.Id)
            {
                //AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _orderItems));
                orderStatusId = OrderStatus.AwaitingValidation.Id;
            }
        }

        public void SetStockConfirmedStatus()
        {
            if (orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                //AddDomainEvent(new OrderStatusChangedToStockConfirmedDomainEvent(Id));

                orderStatusId = OrderStatus.StockConfirmed.Id;
                Description = "All the items were confirmed with available stock.";
            }
        }

        public void SetPaidStatus()
        {
            if (orderStatusId == OrderStatus.StockConfirmed.Id)
            {
                //AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, OrderItems));

                orderStatusId = OrderStatus.Paid.Id;
                Description = "The payment was performed at a simulated \"American Bank checking bank account ending on XX35071\"";
            }
        }

        public void SetShippedStatus()
        {
            if (orderStatusId != OrderStatus.Paid.Id)
            {
                StatusChangeException(OrderStatus.Shipped);
            }

            orderStatusId = OrderStatus.Shipped.Id;
            Description = "The order was shipped.";
        }

        public void SetCancelledStatus()
        {
            if (orderStatusId == OrderStatus.Paid.Id ||
                orderStatusId == OrderStatus.Shipped.Id)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            orderStatusId = OrderStatus.Cancelled.Id;
            Description = $"The order was cancelled.";
            //AddDomainEvent(new OrderCancelledDomainEvent(this));
        }

        public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
        {
            if (orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                orderStatusId = OrderStatus.Cancelled.Id;

                var itemsStockRejectedProductNames = OrderItems
                    .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                    .Select(c => c.GetOrderItemProductName());

                var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
                Description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
            }
        }

        private void AddOrderStartedDomainEvent(Guid userId, string userName, int cardTypeId, string cardNumber,
                string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId,
                                                                        cardNumber, cardSecurityNumber,
                                                                        cardHolderName, cardExpiration);

            this.AddDomainEvent(orderStartedDomainEvent);
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
        }

        public decimal GetTotal()
        {
            return _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
        }
    }
}