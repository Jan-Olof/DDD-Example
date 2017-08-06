using DomainLayer.Interfaces;

namespace InfrastructureLayer.Dtos
{
    /// <summary>
    /// The product update data transfer object.
    /// </summary>
    public class ProductUpdate : IProductUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdate"/> class.
        /// </summary>
        public ProductUpdate()
        {
            Description = string.Empty;
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}