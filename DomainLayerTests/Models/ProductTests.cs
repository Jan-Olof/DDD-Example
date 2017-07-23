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
            var sut = CreateProduct();

            // Act
            var result = sut.Get(2);

            // Assert
            var product = SampleProducts.CreateProducts().SingleOrDefault(result.Compile());

            Assert.AreEqual("SecondProduct", product.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange
            var sut = CreateProduct();

            // Act
            var result = sut.Get("SecondProduct");

            // Assert
            var product = SampleProducts.CreateProducts().SingleOrDefault(result.Compile());

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
            var sut = CreateProduct();

            // Act
            var result = sut.Search("Second");

            // Assert
            var products = SampleProducts.CreateProducts().Where(result.Compile());

            Assert.AreEqual(2, products.Single().Id);
        }

        private static IProduct CreateProduct()
        {
            return new Product();
        }
    }
}