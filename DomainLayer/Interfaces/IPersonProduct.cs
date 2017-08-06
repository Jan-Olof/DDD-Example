using DomainLayer.Enums;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person in a product.
    /// </summary>
    public interface IPersonProduct : IPersonCreate
    {
        /// <summary>
        /// Gets or sets the role of the person.
        /// </summary>
        Role Role { get; set; }
    }
}