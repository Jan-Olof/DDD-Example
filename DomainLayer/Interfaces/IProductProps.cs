namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product properties interface.
    /// </summary>
    public interface IProductProps : IIdentifier
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the name. This is the name of the product.
        /// </summary>
        string Name { get; set; }
    }
}