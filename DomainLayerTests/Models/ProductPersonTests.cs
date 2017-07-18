// ReSharper disable UnusedMember.Global
using System.Linq;
using DomainLayer.Enums;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class ProductPersonTests
    {
        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            var sut = CreateProductPerson();

            // Act
            var result = sut.Get(2, 2, Role.Director);

            // Assert
            var productPerson = SampleProductPerson.CreateProductPersons().Single(result.Compile());

            Assert.AreEqual(Role.Director, productPerson.Role);
        }

        private static IProductPerson CreateProductPerson()
        {
            return new ProductPerson();
        }
    }
}