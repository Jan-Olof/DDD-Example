using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the product base model.
    /// </summary>
    public abstract class ProductBase<T> : Entity<T>, IProductBase<T> where T : IProductBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBase{T}"/> class.
        /// </summary>
        protected ProductBase()
        {
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Updates the fields that are supposed to be updated when editing a product.
        /// </summary>
        public T MapUpdate(T from, T to)
        {
            to.Name = from.Name;
            to.Description = from.Description;

            return to;
        }
    }
}