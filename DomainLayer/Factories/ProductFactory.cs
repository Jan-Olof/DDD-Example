using DomainLayer.Models;

namespace DomainLayer.Factories
{
    /// <summary>
    /// Builds products
    /// </summary>
    public static class ProductFactory
    {
        /// <summary>
        /// Build a product.
        /// </summary>
        public static Product CreateProduct(string name, string description, int id = 0)
        {
            return new Product
            {
                Id = id,
                Name = name,
                Description = description
            };
        }
    }
}