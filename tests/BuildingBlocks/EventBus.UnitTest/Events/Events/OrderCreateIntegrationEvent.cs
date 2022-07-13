using EventBus.Base.Events;

namespace EventBus.UnitTest.Events.Events
{
    public class OrderCreateIntegrationEvent : IntegrationEvent
    {
        public OrderCreateIntegrationEvent(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}