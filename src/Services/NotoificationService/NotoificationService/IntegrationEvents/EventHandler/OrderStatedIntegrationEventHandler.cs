using EventBus.Base.Abstrasctions;
using Microsoft.Extensions.Logging;
using NotoificationService.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace NotoificationService.IntegrationEvents.EventHandler
{
    public class OrderPaymnetFailedIntegrationEventHandlerHandler : IIntegrationEventHandler<OrderPaymnetFailedIntegrationEvent>
    {
        private readonly ILogger<OrderPaymnetFailedIntegrationEvent> logger;

        public Task Handle(OrderPaymnetFailedIntegrationEvent @event)
        {
            logger.LogInformation($"Order Payment Success with OrderId {@event.OrderId}");
            return Task.CompletedTask;
        }
    }
}