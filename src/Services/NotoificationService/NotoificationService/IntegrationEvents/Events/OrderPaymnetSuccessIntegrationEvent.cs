using EventBus.Base.Events;

namespace NotoificationService.IntegrationEvents.Events
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