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
        public void TestShouldDeleteEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            sut.Delete(SampleProducts.CreateProduct(1));

            // Assert
            Assert.AreEqual(2, sut.Get().Count());
        }

        [TestMethod]
        public void TestShouldFillDataSet()
        {
            // Arrange
            var sut = CreateInMemoryRepository();

            // Act
            sut.FillDataSet();

            // Assert
            Assert.AreEqual(2, sut.Get().Count());
        }

        [TestMethod]
        public void TestShouldGetAllEntities()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.Get().ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("FirstProduct", result.Single(n => n.Id == 1).Name);
        }

        [TestMethod]
        public void TestShouldGetEntities()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.Get(n => n.Id == 2).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("SecondProduct", result.Single().Name);
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreNone()
        {
            // Arrange
            var sut = CreateInMemoryRepository();

            // Act
            var result = sut.Insert(SampleProducts.CreateProduct());

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("FirstProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            var result = sut.Insert(SampleProducts.CreateProduct());

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

            Assert.AreEqual(4, sut2.Get().Count());
        }

        [TestMethod]
        public void TestShouldUpdateEntity()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleProducts.CreateProducts());

            // Act
            sut.Update(SampleProducts.CreateProduct(2, "Updated name", "Updated description."));

            // Assert
            Assert.AreEqual("Updated name", sut.Get(e => e.Id == 2).Single().Name);
            Assert.AreEqual("Updated description.", sut.Get(e => e.Id == 2).Single().Description);
        }

        private static IRepository<Product> CreateInMemoryRepository()
        {
            return new InMemoryRepository(
                new FileHandler<IList<Product>>(CreateDatafileOptions(), new JsonSerialization()));
        }

        private static IRepository<Product> CreateInMemoryRepository(IList<Product> products)
        {
            var repository = CreateInMemoryRepository();

            foreach (var product in products)
            {
                repository.Insert(product);
            }

            return repository;
        }
    }
}