namespace ApplicationLayer.EventLogging
{
    public class EventObject
    {
        public object Entity { get; set; }
        public string EntityName { get; set; }
        public string Type { get; set; }
    }
}