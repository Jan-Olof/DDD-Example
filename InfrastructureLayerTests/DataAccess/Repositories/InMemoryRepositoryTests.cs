// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Models;
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
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            sut.Delete(SampleInstructions.CreateInstruction(1));

            // Assert
            Assert.AreEqual(2, sut.Get().Count());
        }

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

        [TestMethod]
        public void TestShouldUpdateEntity()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            sut.Update(SampleInstructions.CreateInstruction(2, "Updated name", "Updated description."), i => i.Id == 2);

            // Assert
            Assert.AreEqual("Updated name", sut.Get(e => e.Id == 2).Single().Name);
            Assert.AreEqual("Updated description.", sut.Get(e => e.Id == 2).Single().Description);
        }

        // TODO: Add tests for Fill and Persist.

        private IRepository<IInstruction> CreateInMemoryRepository()
        {
            return new InMemoryRepository(new Instruction(), new FileHandler<IList<IInstruction>>(CreateDatafileOptions(), new JsonSerialization()));
        }

        private IRepository<IInstruction> CreateInMemoryRepository(IList<IInstruction> instructions)
        {
            return new InMemoryRepository(new Instruction(), instructions, new FileHandler<IList<IInstruction>>(CreateDatafileOptions(), new JsonSerialization()));
        }
    }
}