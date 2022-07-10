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
        private readonly IEventBusSubscriptionManager subManager;
        private  EventBusConfig config;

        protected BaseEventBus(IServiceProvider serviceProvider, IEventBusSubscriptionManager eventBusSubscriptionManager, EventBusConfig config)
        {
            _serviceProvider = serviceProvider;
            this.subManager = new InMemeoryEventBusSubscriptionManager(ProccessEventName);
            this.config = config;
        }

        public abstract void Publish(IntegrationEvent @event);
        
        public virtual string ProccessEventName(string name)
        {
            if (config.DeleteEventPreFix)
                name = name.TrimStart(config.EventNamePreFix.ToArray());
            if (config.DeleteEventeSuffix)
                name = name.TrimStart(config.EventNameSuffix.ToArray());
            return name;
        }
        public virtual string GetSubName(string name)
        {
            return $"{config.SubscriptionClinetAppName}.{ProccessEventName(name)}";
        }
        public abstract void SubScribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        
        public virtual void Dispose()
        {
            config = null;
        }
        public async Task<bool> ProccessEvent(string name,string message)
        {
            var eventName=ProccessEventName(name);
            var proccess = false;
            if (subManager.HasSubScriptionsForEvent(name))
            {
                var subscriptions = subManager.GetHadlersForEvent(name);
                using (var scop= _serviceProvider.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {
                        var handler = _serviceProvider.GetService(subscription.HandleType);
                        if (handler == null) continue;
                        var eventType = subManager.GetEventTypeByName($"{config.EventNamePreFix}{eventName}{config.EventNameSuffix}");
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