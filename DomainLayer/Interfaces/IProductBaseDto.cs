namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product properties interface.
    /// </summary>
    public interface IProductBaseDto : IIdentifier, IName
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        string Description { get; set; }
    }
}