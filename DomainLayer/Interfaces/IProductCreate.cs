namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The interface of what you need when you create a new product.
    /// </summary>
    public interface IProductCreate : IName
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        string Description { get; set; }
    }
}