using EventBus.Base.Events;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderPaymnetSuccessIntegrationEvent : IntegrationEvent
    {
        public OrderPaymnetSuccessIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}