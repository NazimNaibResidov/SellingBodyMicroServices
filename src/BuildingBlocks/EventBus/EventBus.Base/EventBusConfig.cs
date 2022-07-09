using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public class EventBusConfig
    {
        public int ConnectionRetryCount { get; set; } = 6;
        public string DefultTopicName { get; set; } = "Naib";
        public string EventBusConnectionString { get; set; } = String.Empty;
        public string SubscriptionClinetAppName { get; set; } = String.Empty;
        public string EventNamePreFix { get; set; } = String.Empty;
        public string EventNameSuffix { get; set; } = "IntegrationEvent";
        public object Connection { get; set; }
        public EventBusType eventBusType { get; set; } = EventBusType.RabbitMq;
        public bool DeleteEventPreFix => !String.IsNullOrEmpty(EventNamePreFix);
        public bool DeleteEventeuffix => !String.IsNullOrEmpty(EventNameSuffix);

        public bool DeleteEventeSuffix { get; internal set; }
    }
}
