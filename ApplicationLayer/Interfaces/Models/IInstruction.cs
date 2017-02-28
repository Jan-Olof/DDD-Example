namespace ApplicationLayer.Interfaces.Models
{
    public interface IInstruction : IIdentifier
    {
        string Description { get; set; }
        string Name { get; set; }
    }
}