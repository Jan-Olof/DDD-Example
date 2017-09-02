using DomainLayer.Enums;
using InfrastructureLayerTests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InfrastructureLayerTests.DataAccess.Daos
{
    [TestClass]
    public class ProductPersonTests
    {
        [TestMethod]
        public void TestShouldCompareList1AndList1()
        {
            // Arrange

            var pp1 = SampleProductPersons.CreateProductPersons();

            // Act
            var result = pp1.Except(pp1).ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestShouldCompareList1AndList2()
        {
            // Arrange

            var pp1 = SampleProductPersons.CreateProductPersons();
            var pp2 = SampleProductPersons.CreateProductPersons2();

            // Act
            var result = pp1.Except(pp2).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(Role.Actor, result.Single(p => p.PersonId == 1 && p.ProductId == 2).Role);
            Assert.AreEqual(Role.Writer, result.Single(p => p.PersonId == 2 && p.ProductId == 3).Role);
        }

        [TestMethod]
        public void TestShouldCompareList2AndList1()
        {
            // Arrange

            var pp1 = SampleProductPersons.CreateProductPersons();
            var pp2 = SampleProductPersons.CreateProductPersons2();

            // Act
            var result = pp2.Except(pp1).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Role.Producer, result.Single(p => p.PersonId == 2 && p.ProductId == 3).Role);
        }
    }
}