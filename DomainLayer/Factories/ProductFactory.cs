using DomainLayer.Enums;
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

        /// <summary>
        /// Build a product in a person.
        /// </summary>
        public static ProductInPerson CreateProductInPerson(int id, string name, string description, Role role)
        {
            return new ProductInPerson
            {
                Id = id,
                Name = name,
                Description = description,
                Role = role
            };
        }
    }
}