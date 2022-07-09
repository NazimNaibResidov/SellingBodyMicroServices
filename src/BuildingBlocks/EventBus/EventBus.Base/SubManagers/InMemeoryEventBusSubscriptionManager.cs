using EventBus.Base.Abstrasctions;
using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus.Base.SubManagers
{
    public class InMemeoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public Func<string, string> eventNameGetter;

        public InMemeoryEventBusSubscriptionManager(Func<string, string> eventNameGetter)
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
            this.eventNameGetter = eventNameGetter;
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscription<T, TH>()
          where T : IntegrationEvent
          where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            AddSubscription(typeof(TH), eventName);
            if (_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public string GetEventKey<T>()
        {
            string eventName = typeof(T).Name;
            return eventNameGetter(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHadlersForEvent<T>() where T : IntegrationEvent
        {
            var eventName = GetEventKey<T>();
            return GetHadlersForEvent(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHadlersForEvent(string eventName) => _handlers[eventName];

        public bool HasSubScriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubScriptionsForEvent(key);
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            RemoveHandler(eventName, handlerToRemove);
        }

        public SubscriptionInfo FindSubscriptionToRemove<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return FindSubscriptionToRemove(eventName, typeof(TH));
        }

        public bool HasSubScriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(x => x.Name == eventName);

        #region ::AddSubscription::

        private void AddSubscription(Type handerType, string eventName)
        {
            if (!HasSubScriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }
            if (_handlers[eventName].Any(s => s.HandleType == handerType))
            {
                throw new ArgumentNullException($" Handler Type {handerType.Name} alredy registred for {eventName}", nameof(handerType));
            }
            _handlers[eventName].Add(SubscriptionInfo.Typed(handerType));
        }

        private SubscriptionInfo FindSubscriptionToRemove(string eventName, Type typeHandler)
        {
            if (!HasSubScriptionsForEvent(eventName))
            {
                return null;
            }
            return _handlers[eventName].SingleOrDefault(s => s.HandleType == typeHandler);
        }

        private void RemoveHandler(string eventName, SubscriptionInfo subscriptionInfo)
        {
            if (subscriptionInfo != null)
            {
                _handlers[eventName].Remove(subscriptionInfo);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(s => s.Name == eventName);
                    if (eventName != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

       

        #endregion ::AddSubscription::
    }
}