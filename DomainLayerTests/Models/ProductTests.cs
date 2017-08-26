// ReSharper disable UnusedMember.Global
using System.Linq;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            var products = SampleProducts.CreateProducts();

            // Act
            var result = Product.Get(2);

            // Assert
            var product = products.SingleOrDefault(result.Compile());

            Assert.IsNotNull(product);
            Assert.AreEqual("SecondProduct", product.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange
            var products = SampleProducts.CreateProducts();

            // Act
            var result = Product.Get("SecondProduct");

            // Assert
            var product = products.SingleOrDefault(result.Compile());

            Assert.IsNotNull(product);
            Assert.AreEqual(2, product.Id);
        }

        [TestMethod]
        public void TestShouldMapUpdate()
        {
            // Arrange
            var sut = CreateProduct();

            // Act
            var result = sut.MapUpdate(SampleProducts.CreateProduct(33, "SecondProduct", "This is the second product."), SampleProducts.CreateProduct(2));

            // Assert
            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("SecondProduct", result.Name);
            Assert.AreEqual("This is the second product.", result.Description);
        }

        [TestMethod]
        public void TestShouldSearchProductFromName()
        {
            // Arrange
            var allProducts = SampleProducts.CreateProducts();

            // Act
            var result = Product.Search("Second");

            // Assert
            var products = allProducts.Where(result.Compile());

            Assert.AreEqual(2, products.Single().Id);
        }

        private static IProduct CreateProduct()
        {
            return new Product();
        }
    }
}