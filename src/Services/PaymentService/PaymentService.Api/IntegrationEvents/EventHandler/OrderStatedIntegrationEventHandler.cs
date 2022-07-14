using EventBus.Base.Abstrasctions;
using EventBus.Base.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentService.Api.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace PaymentService.Api.IntegrationEvents.EventHandler
{
    public class OrderStatedIntegrationEventHandler : IIntegrationEventHandler<OrderStatedIntegrationEvent>
    {
        private readonly IConfiguration configuration;
        private readonly IEventBus eventBus;
        private readonly ILogger<OrderStatedIntegrationEvent> logger;

        public OrderStatedIntegrationEventHandler(IConfiguration configuration, IEventBus eventBus, ILogger<OrderStatedIntegrationEvent> logger)
        {
            this.configuration = configuration;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        public Task Handle(OrderStatedIntegrationEvent @event)
        {
            string keyword = "PaymentSuccess";
            bool PaymnetSuccessFlag = configuration.GetValue<bool>(keyword);
            IntegrationEvent paymentEvent = PaymnetSuccessFlag
                ? new OrderPaymnetSuccessIntegrationEvent(@event.OrderId)
                : new OrderPaymnetFailedIntegrationEvent(@event.OrderId, "this is fake message");
            logger.LogInformation($"OrderStatedIntegrationEventHandler is failed with PaymentSuccess {PaymnetSuccessFlag}");
            eventBus.Publish(paymentEvent);
            return Task.CompletedTask;
        }
    }
}