using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SampleProducts
    {
        public static Product CreateProduct(
            int id = 0, string name = "FirstProduct", string description = "This is the first product.")
        {
            return new Product
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<Product> CreateProducts()
        {
            return new List<Product>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product."),
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static List<Product> CreateProducts2()
        {
            return new List<Product>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product.")
            };
        }

        public static List<Product> CreateProducts3()
        {
            return new List<Product>
            {
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static IList<Product> CreateProducts4()
        {
            return new List<Product>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product."),
                CreateProduct(3, "ThirdProduct", "This is the third product."),
                CreateProduct(4, "FourthProduct", "This is the fourth product.")
            };
        }

        public static List<Product> CreateProductsDuplicate()
        {
            return new List<Product>
            {
                CreateProduct(3, "ThirdProduct", "This is the third product."),
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }
    }
}