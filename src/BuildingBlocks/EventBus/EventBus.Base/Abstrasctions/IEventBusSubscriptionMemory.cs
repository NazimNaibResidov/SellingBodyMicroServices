using EventBus.Base.Events;
using System;
using System.Collections.Generic;

namespace EventBus.Base.Abstrasctions
{
    public interface IEventBusSubscriptionManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

        void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

        bool HasSubScriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubScriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHadlersForEvent<T>() where T : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHadlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}