using DomainLayer.Models;

namespace DomainLayer.Factories
{
    public static class ProductFactory
    {
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