using EventBus.Base.Events;
using System.Threading.Tasks;

namespace EventBus.Base.Abstrasctions
{
    public interface IIntegrationEventHandler<T> : IntegrationEventHandler where T : IntegrationEvent
    {
        Task Handle(T @event);
    }

    public interface IntegrationEventHandler
    {
    }
}