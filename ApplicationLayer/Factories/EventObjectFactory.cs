using ApplicationLayer.EventLogging;
using DomainLayer.Models;

namespace ApplicationLayer.Factories
{
    /// <summary>
    /// Builds event objects used in event logging.
    /// </summary>
    public static class EventObjectFactory
    {
        /// <summary>
        /// Build an event object from a product.
        /// </summary>
        public static EventObject CreateEventObject(Product product, EventType eventType)
        {
            return new EventObject
            {
                Entity = product,
                EntityName = typeof(Product).ToString(),
                Type = eventType.ToString()
            };
        }

        /// <summary>
        /// Build an event object from a person.
        /// </summary>
        public static EventObject CreateEventObject(Person person, EventType eventType)
        {
            return new EventObject
            {
                Entity = person,
                EntityName = typeof(Person).ToString(),
                Type = eventType.ToString()
            };
        }
    }
}