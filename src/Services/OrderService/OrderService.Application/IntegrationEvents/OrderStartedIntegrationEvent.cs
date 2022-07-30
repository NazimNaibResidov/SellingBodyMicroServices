using EventBus.Base.Events;

namespace OrderService.Application.IntegrationEvents

{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public OrderStartedIntegrationEvent(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}