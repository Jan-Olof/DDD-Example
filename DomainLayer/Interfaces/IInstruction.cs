namespace DomainLayer.Interfaces
{
    public interface IInstruction
    {
        string Description { get; set; }
        int Id { get; set; }
        string Name { get; set; }
    }
}