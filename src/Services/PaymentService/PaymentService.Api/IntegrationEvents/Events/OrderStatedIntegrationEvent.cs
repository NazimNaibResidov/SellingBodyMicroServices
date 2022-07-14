using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStatedIntegrationEvent : IntegrationEvent
    {
        public OrderStatedIntegrationEvent()
        {
        }

        public OrderStatedIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}