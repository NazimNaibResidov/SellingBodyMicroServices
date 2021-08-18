using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Abstrasctions
{
    public interface IEventBusSubscriptionMemory
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddSubscription<T, TH>() where T: IntegrationEvent where TH:IIntegrationEventHandler<T>;
        void RemoveSubscription<T,TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void HasSubScriptionsForEvent<T>() where T : IntegrationEvent;
        void HasSubScriptionsForEvent(string eventName);
        void GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHadlersForEvent<T>() where T:IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHadlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
