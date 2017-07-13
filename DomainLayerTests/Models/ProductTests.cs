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
            var instruction = SampleProducts.CreateProducts().SingleOrDefault(result.Compile());

            Assert.AreEqual("SecondProduct", instruction.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange

            var sut = CreateProduct();

            // Act
            var result = sut.Get("SecondProduct");

            // Assert
            var instruction = SampleProducts.CreateProducts().SingleOrDefault(result.Compile());

            Assert.AreEqual(2, instruction.Id);
        }

        private static IProduct CreateProduct()
        {
            return new Product();
        }
    }
}