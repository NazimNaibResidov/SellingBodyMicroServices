using Newtonsoft.Json;
using System;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid ID { get; private set; }

        [JsonProperty]
        public DateTime CreateDate { get; private set; }

        [JsonConstructor]
        public IntegrationEvent()
        {
            ID = new Guid();
            CreateDate = DateTime.Now;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            CreateDate = createDate;
            ID = id;
        }
    }
}