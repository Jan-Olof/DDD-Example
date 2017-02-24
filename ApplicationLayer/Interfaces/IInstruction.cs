namespace ApplicationLayer.Interfaces
{
    public interface IInstruction : IDto
    {
        string Description { get; set; }
        string Name { get; set; }
    }
}