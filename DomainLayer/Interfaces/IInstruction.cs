using Utilities.Interfaces;

namespace DomainLayer.Interfaces
{
    public interface IInstruction : IDto
    {
        string Description { get; set; }
        string Name { get; set; }
    }
}