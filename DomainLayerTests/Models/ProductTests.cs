// ReSharper disable UnusedMember.Global
using DomainLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            var result = Entity.Get<Product>(2);

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
            var result = Entity.Get<Product>("SecondProduct");

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
            var result = Entity.Search<Product>("Second");

            // Assert
            var products = allProducts.Where(result.Compile());

            Assert.AreEqual(2, products.Single().Id);
        }

        private static IProduct CreateProduct()
            => new Product();
    }
}