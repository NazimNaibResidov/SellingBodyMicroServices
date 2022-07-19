using EventBus.Base.Abstrasctions;
using Microsoft.Extensions.Logging;
using NotoificationService.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace NotoificationService.IntegrationEvents.EventHandler
{
    public class OrderPaymnetSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymnetSuccessIntegrationEvent>
    {
        private readonly ILogger<OrderPaymnetSuccessIntegrationEvent> logger;

        public OrderPaymnetSuccessIntegrationEventHandler(ILogger<OrderPaymnetSuccessIntegrationEvent> logger)
        {
            this.logger = logger;
        }

        public Task Handle(OrderPaymnetSuccessIntegrationEvent @event)
        {
            logger.LogInformation($"Order Payment Success with OrderId {@event.OrderId}");
            return Task.CompletedTask;
        }
    }
}