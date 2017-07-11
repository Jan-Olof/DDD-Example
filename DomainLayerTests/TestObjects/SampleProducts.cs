using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayerTests.TestObjects
{
    public static class SampleProducts
    {
        public static Product CreateProduct(
            int id = 0, string name = "FirstInstruction", string description = "This is the first instruction.")
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
                CreateProduct(2, "SecondInstruction", "This is the second instruction."),
                CreateProduct(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static List<Product> CreateProducts2()
        {
            return new List<Product>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondInstruction", "This is the second instruction.")
            };
        }

        public static List<Product> CreateProducts3()
        {
            return new List<Product>
            {
                CreateProduct(3, "ThirdInstruction", "This is the third instruction.")
            };
        }

        public static IList<Product> CreateProducts4()
        {
            return new List<Product>
            {
                CreateProduct(1),
                CreateProduct(2, "SecondInstruction", "This is the second instruction."),
                CreateProduct(3, "ThirdInstruction", "This is the third instruction."),
                CreateProduct(4, "FourthInstruction", "This is the fourth instruction.")
            };
        }

        public static List<Product> CreateProductsDuplicate()
        {
            return new List<Product>
            {
                CreateProduct(3, "ThirdInstruction", "This is the third instruction."),
                CreateProduct(3, "ThirdInstruction", "This is the third instruction.")
            };
        }
    }
}