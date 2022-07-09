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
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBusSubscriptionManager eventBusSubscriptionManager;
        private EventBusConfig EventBusConfig;

        protected BaseEventBus(IServiceProvider serviceProvider, IEventBusSubscriptionManager eventBusSubscriptionManager, EventBusConfig eventBusConfig)
        {
            _serviceProvider = serviceProvider;
            this.eventBusSubscriptionManager = new InMemeoryEventBusSubscriptionManager(ProccessEventName);
            EventBusConfig = eventBusConfig;
        }

        public abstract void Publish(IntegrationEvent @event);
        
        public virtual string ProccessEventName(string name)
        {
            if (EventBusConfig.DeleteEventPreFix)
                name = name.TrimStart(EventBusConfig.EventNamePreFix.ToArray());
            if (EventBusConfig.DeleteEventeSuffix)
                name = name.TrimStart(EventBusConfig.EventNameSuffix.ToArray());
            return name;
        }
        public virtual string GetSubName(string name)
        {
            return $"{EventBusConfig.SubscriptionClinetAppName}.{ProccessEventName(name)}";
        }
        public abstract void SubScribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        
        public virtual void Dispose()
        {
            EventBusConfig = null;
        }
        public async Task<bool> ProccessEvent(string name,string message)
        {
            var eventName=ProccessEventName(name);
            var proccess = false;
            if (eventBusSubscriptionManager.HasSubScriptionsForEvent(name))
            {
                var subscriptions = eventBusSubscriptionManager.GetHadlersForEvent(name);
                using (var scop= _serviceProvider.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {
                        var handler = _serviceProvider.GetService(subscription.HandleType);
                        if (handler == null) continue;
                        var eventType = eventBusSubscriptionManager.GetEventTypeByName($"{EventBusConfig.EventNamePreFix}{eventName}{EventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                        var concreateType=typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreateType.GetMethod("Handle").Invoke(handler,new object[] { integrationEvent });
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