using EventBus.Base.Abstrasctions;
using Microsoft.Extensions.Logging;
using NotoificationService.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace NotoificationService.IntegrationEvents.EventHandler
{
    public class OrderStatedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymnetFailedIntegrationEvent>
    {
        private readonly ILogger<PaymentService.Api.IntegrationEvents.Events.OrderStatedIntegrationEvent> logger;

        public Task Handle(OrderPaymnetFailedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}