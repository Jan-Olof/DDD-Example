using ApplicationLayer.EventLogging;

namespace ApplicationLayer.Factories
{
    /// <summary>
    /// Builds event objects used in event logging.
    /// </summary>
    public static class EventObjectFactory<T>
    {
        /// <summary>
        /// Build an event object from an entity.
        /// </summary>
        public static EventObject CreateEventObject(T person, EventType eventType)
        {
            return new EventObject
            {
                Entity = person,
                EntityName = typeof(T).ToString(),
                Type = eventType.ToString()
            };
        }
    }
}