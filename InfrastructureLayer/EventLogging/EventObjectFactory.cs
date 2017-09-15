namespace InfrastructureLayer.EventLogging
{
    /// <summary>
    /// Builds event objects used in event logging.
    /// </summary>
    public static class EventObjectFactory<T>
    {
        /// <summary>
        /// Build an event object from an entity.
        /// </summary>
        public static EventObject CreateEventObject(T entity, EventType eventType)
            => new EventObject(entity, typeof(T).ToString(), eventType.ToString());
    }
}