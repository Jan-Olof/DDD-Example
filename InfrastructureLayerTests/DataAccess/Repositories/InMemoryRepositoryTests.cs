using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class InMemoryRepositoryTests
    {
        [TestMethod]
        public void TestShouldGetAllEntities()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            var result = sut.Get().ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("FirstInstruction", result.Single(n => n.Id == 1).Name);
        }

        [TestMethod]
        public void TestShouldGetEntities()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            var result = sut.Get(n => n.Id == 2).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("SecondInstruction", result.Single().Name);
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreNone()
        {
            // Arrange
            var sut = CreateInMemoryRepository();

            // Act
            var result = sut.Insert(SampleInstructions.CreateInstruction());

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("FirstInstruction", result.Name);
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            var result = sut.Insert(SampleInstructions.CreateInstruction());

            // Assert
            Assert.AreEqual(4, result.Id);
            Assert.AreEqual("FirstInstruction", result.Name);
        }

        private static InMemoryRepository<IInstruction> CreateInMemoryRepository()
        {
            return new InMemoryRepository<IInstruction>();
        }

        private static InMemoryRepository<IInstruction> CreateInMemoryRepository(IList<IInstruction> instructions)
        {
            return new InMemoryRepository<IInstruction>(instructions);
        }
    }
}