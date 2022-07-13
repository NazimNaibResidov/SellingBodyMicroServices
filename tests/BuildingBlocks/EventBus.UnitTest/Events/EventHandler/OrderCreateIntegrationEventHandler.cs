using EventBus.Base.Abstrasctions;
using EventBus.UnitTest.Events.Events;

namespace EventBus.UnitTest.Events.EventHandler
{
    public class OrderCreateIntegrationEventHandler : IIntegrationEventHandler<OrderCreateIntegrationEvent>
    {
        public Task Handle(OrderCreateIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}