using DomainLayer.Models;

namespace DomainLayer.Factories
{
    public static class ProductFactory
    {
        public static Product CreateProduct(string name, string description)
        {
            return new Product
            {
                Name = name,
                Description = description
            };
        }
    }
}