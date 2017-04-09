// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer.Interfaces.Infrastructure;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class InMemoryRepositoryTests
    {
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

        private static IRepository<IInstruction> CreateInMemoryRepository()
        {
            return new InMemoryRepository<IInstruction>(new Instruction());
        }

        private static IRepository<IInstruction> CreateInMemoryRepository(IList<IInstruction> instructions)
        {
            return new InMemoryRepository<IInstruction>(new Instruction(), instructions);
        }
    }
}