using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Factories
{
    public static class EventIdFactory
    {
        public static EventId CreatePersistenceEventId()
        {
            return new EventId(4, "Persistence Event");
        }

        public static EventId CreatePersonEventId()
        {
            return new EventId(3, "Person Event");
        }

        public static EventId CreateProductEventId()
        {
            return new EventId(2, "Product Event");
        }

        public static EventId CreateUiEventId()
        {
            return new EventId(1, "User Interface Event");
        }
    }
}