namespace NotoificationService.IntegrationEvents.Events
{
    public class OrderPaymnetFailedIntegrationEvent : IntegrationEvent
    {
        public OrderPaymnetFailedIntegrationEvent(int orderId, string errorMessage)
        {
            ErrorMessage = errorMessage;
            OrderId = orderId;
        }

        public string ErrorMessage { get; set; }
        public int OrderId { get; set; }
    }
}