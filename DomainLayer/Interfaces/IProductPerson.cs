using DomainLayer.Enums;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// A product that a person is in.
    /// </summary>
    public interface IProductPerson : IProductUpdate
    {
        /// <summary>
        /// Gets or sets the role in the product.
        /// </summary>
        Role Role { get; set; }
    }
}