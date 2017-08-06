using DomainLayer.Interfaces;

namespace InfrastructureLayer.Dtos
{
    /// <summary>
    /// The product create data transfer object.
    /// </summary>
    public class ProductCreate : IProductCreate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreate"/> class.
        /// </summary>
        public ProductCreate()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}