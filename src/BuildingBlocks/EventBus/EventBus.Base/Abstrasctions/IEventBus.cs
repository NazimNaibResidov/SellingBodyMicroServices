using EventBus.Base.Events;

namespace EventBus.Base.Abstrasctions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        void SubScribe<T, TH>() where T: IntegrationEvent where TH:IIntegrationEventHandler<T>;
        void UnSubScribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    }
}
