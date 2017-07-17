// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Linq;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer.Interfaces.Infrastructure;
using InfrastructureLayer.Files;
using static InfrastructureLayerTests.TestObjects.TestFactory;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class InMemoryRepositoryTests
    {
        [TestInitialize]
        public void SetUp()
        {
            RestoreFileContent();
        }

        [TestCleanup]
        public void TearDown()
        {
            RestoreFileContent();
        }

        [TestMethod]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            sut.DeleteProduct(SampleProducts.CreateProduct(1));

            // Assert
            Assert.AreEqual(2, sut.GetProducts().Count());
        }

        [TestMethod]
        public void TestShouldDeleteProductUsingId()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            sut.DeleteProduct(1);

            // Assert
            Assert.AreEqual(2, sut.GetProducts().Count());
        }

        [TestMethod]
        public void TestShouldFillDataSet()
        {
            // Arrange
            var sut = CreateInMemoryRepository();

            // Act
            sut.FillDataSet();

            // Assert
            Assert.AreEqual(2, sut.GetProducts().Count());
        }

        [TestMethod]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.GetProducts().ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("FirstProduct", result.Single(n => n.Id == 1).Name);
        }

        [TestMethod]
        public void TestShouldGetProducts()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.GetProducts(n => n.Id == 2).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("SecondProduct", result.Single().Name);
        }

        [TestMethod]
        public void TestShouldInsertProductWhenThereAreNone()
        {
            // Arrange
            var sut = CreateInMemoryRepository();

            // Act
            var result = sut.InsertProduct(SampleProducts.CreateProduct());

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("FirstProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldInsertProductWhenThereAreSomeAlready()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.InsertProduct(SampleProducts.CreateProduct());

            // Assert
            Assert.AreEqual(4, result.Id);
            Assert.AreEqual("FirstProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldPersistData()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts4());

            // Act
            sut.PersistData();

            // Assert
            var sut2 = CreateInMemoryRepository();
            sut2.FillDataSet();

            Assert.AreEqual(4, sut2.GetProducts().Count());
        }

        [TestMethod]
        public void TestShouldUpdateProduct()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            sut.UpdateProduct(SampleProducts.CreateProduct(2, "Updated name", "Updated description."));

            // Assert
            Assert.AreEqual("Updated name", sut.GetProducts(e => e.Id == 2).Single().Name);
            Assert.AreEqual("Updated description.", sut.GetProducts(e => e.Id == 2).Single().Description);
        }

        private static IDomainRepository CreateInMemoryRepository()
        {
            return new InMemoryRepository(
                new FileHandler<IList<Product>>(CreateDatafileOptions(), new JsonSerialization()));
        }

        private static IDomainRepository CreateInMemoryRepository(IList<Product> products)
        {
            var repository = CreateInMemoryRepository();

            foreach (var product in products)
            {
                repository.InsertProduct(product);
            }

            return repository;
        }
    }
}