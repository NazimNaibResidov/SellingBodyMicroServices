using EventBus.Base.Events;

namespace EventBus.Base.Abstrasctions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

        void UnSubScribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}