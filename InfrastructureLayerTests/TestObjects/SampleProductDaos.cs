using InfrastructureLayer.DataAccess.Daos;
using System.Collections.Generic;

namespace InfrastructureLayerTests.TestObjects
{
    public static class SampleProductDaos
    {
        public static ProductDao CreateProduct(
            int id = 0, string name = "FirstProduct", string description = "This is the first product.")
        {
            return new ProductDao
            {
                Description = description,
                Id = id,
                Name = name
            };
        }

        public static IList<ProductDao> CreateProducts()
        {
            return new List<ProductDao>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product."),
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static List<ProductDao> CreateProducts2()
        {
            return new List<ProductDao>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product.")
            };
        }

        public static List<ProductDao> CreateProducts3()
        {
            return new List<ProductDao>
            {
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }

        public static IList<ProductDao> CreateProducts4()
        {
            return new List<ProductDao>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondProduct", "This is the second product."),
                CreateProduct(3, "ThirdProduct", "This is the third product."),
                CreateProduct(4, "FourthProduct", "This is the fourth product.")
            };
        }

        public static List<ProductDao> CreateProductsDuplicate()
        {
            return new List<ProductDao>
            {
                CreateProduct(3, "ThirdProduct", "This is the third product."),
                CreateProduct(3, "ThirdProduct", "This is the third product.")
            };
        }
    }
}