using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Factories
{
    /// <summary>
    /// Builds EventIds for use in logging.
    /// </summary>
    public static class EventIdFactory
    {
        /// <summary>
        /// Build EventId number 4.
        /// </summary>
        public static EventId CreatePersistenceEventId()
        {
            return new EventId(4, "Persistence Event");
        }

        /// <summary>
        /// Build EventId number 3.
        /// </summary>
        public static EventId CreatePersonEventId()
        {
            return new EventId(3, "Person Event");
        }

        /// <summary>
        /// Build EventId number 2.
        /// </summary>
        public static EventId CreateProductEventId()
        {
            return new EventId(2, "Product Event");
        }

        /// <summary>
        /// Build EventId number 1.
        /// </summary>
        public static EventId CreateUiEventId()
        {
            return new EventId(1, "User Interface Event");
        }
    }
}