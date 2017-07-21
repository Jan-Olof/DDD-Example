using ApplicationLayer.EventLogging;
using DomainLayer.Models;

namespace ApplicationLayer.Factories
{
    public static class EventObjectFactory
    {
        public static EventObject CreateEventObject(Product product, EventType eventType)
        {
            return new EventObject
            {
                Entity = product,
                EntityName = typeof(Product).ToString(),
                Type = eventType.ToString()
            };
        }
    }
}