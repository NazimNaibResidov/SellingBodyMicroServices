using EventBus.Base.Abstrasctions;
using EventBus.Base.SubManagers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {
        public readonly IServiceProvider _serviceProvider;
        public readonly IEventBusSubscriptionManager SubManager;
        public EventBusConfig eventBusConfig;

        protected BaseEventBus(IServiceProvider serviceProvider, EventBusConfig config)
        {
            _serviceProvider = serviceProvider;
            this.SubManager = new InMemeoryEventBusSubscriptionManager(ProccessEventName);
            this.eventBusConfig = config;
        }

        public abstract void Publish(IntegrationEvent @event);

        public virtual string ProccessEventName(string name)
        {
            if (eventBusConfig.DeleteEventPreFix)
                name = name.TrimStart(eventBusConfig.EventNamePreFix.ToArray());
            if (eventBusConfig.DeleteEventeSuffix)
                name = name.TrimStart(eventBusConfig.EventNameSuffix.ToArray());
            return name;
        }

        public virtual string GetSubName(string name)
        {
            return $"{eventBusConfig.SubscriptionClinetAppName}.{ProccessEventName(name)}";
        }

        public abstract void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        public virtual void Dispose()
        {
            eventBusConfig = null;
        }

        public async Task<bool> ProccessEvent(string name, string message)
        {
            var eventName = ProccessEventName(name);
            var proccess = false;
            if (SubManager.HasSubScriptionsForEvent(name))
            {
                var subscriptions = SubManager.GetHadlersForEvent(name);
                using (var scop = _serviceProvider.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {
                        var handler = _serviceProvider.GetService(subscription.HandleType);
                        if (handler == null) continue;
                        var eventType = SubManager.GetEventTypeByName($"{eventBusConfig.EventNamePreFix}{eventName}{eventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                        var concreateType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreateType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
                proccess = true;
            }

            return proccess;
        }

        public abstract void UnSubScribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}