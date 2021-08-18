using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid ID { get;private set; }
        [JsonProperty]
        public DateTime CreateDate { get;private set; }

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
