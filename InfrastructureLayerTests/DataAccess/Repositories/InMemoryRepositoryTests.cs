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
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            sut.Delete(SampleInstructions.CreateInstruction(1));

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
        public void TestShouldPersistData()
        {
            // Arrange
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions4());

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
            var sut = CreateInMemoryRepository(SampleInstructions.CreateInstructions());

            // Act
            sut.Update(SampleInstructions.CreateInstruction(2, "Updated name", "Updated description."), i => i.Id == 2);

            // Assert
            Assert.AreEqual("Updated name", sut.Get(e => e.Id == 2).Single().Name);
            Assert.AreEqual("Updated description.", sut.Get(e => e.Id == 2).Single().Description);
        }

        private static IRepository<Instruction> CreateInMemoryRepository()
        {
            return new InMemoryRepository(
                new Instruction(), new FileHandler<IList<Instruction>>(CreateDatafileOptions(), new JsonSerialization()));
        }

        private static IRepository<Instruction> CreateInMemoryRepository(IList<Instruction> instructions)
        {
            return new InMemoryRepository(
                new Instruction(), instructions, new FileHandler<IList<Instruction>>(CreateDatafileOptions(), new JsonSerialization()));
        }
    }
}