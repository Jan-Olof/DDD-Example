namespace InfrastructureLayer.EventLogging
{
    public class EventObject
    {
        public EventObject(object entity, string entityName, string type)
        {
            Entity = entity;
            EntityName = entityName;
            Type = type;
        }

        public object Entity { get; }
        public string EntityName { get; }
        public string Type { get; }
    }
}