using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Factories
{
    /// <summary>
    /// Builds EventIds for use in logging.
    /// </summary>
    public static class EventIdFactory
    {
        /// <summary>
        /// Build EventId number 5.
        /// </summary>
        public static EventId ApiEventId()
            => new EventId(5, "Application Programming Interface Event");

        /// <summary>
        /// Build EventId number 4.
        /// </summary>
        public static EventId PersistenceEventId()
            => new EventId(4, "Persistence Event");

        /// <summary>
        /// Build EventId number 3.
        /// </summary>
        public static EventId PersonEventId()
            => new EventId(3, "Person Event");

        /// <summary>
        /// Build EventId number 2.
        /// </summary>
        public static EventId ProductEventId()
            => new EventId(2, "Product Event");

        /// <summary>
        /// Build EventId number 1.
        /// </summary>
        public static EventId UiEventId()
            => new EventId(1, "User Interface Event");
    }
}