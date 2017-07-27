using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person interface.
    /// </summary>
    public interface IPerson : IPersonBase<Person>, IPersonDto
    {
    }
}