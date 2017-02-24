using System;
using System.Linq;
using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class EfRepositoryTests
    {
        [TestInitialize]
        public void SetUp()
        {
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetAllEntities()
        {
            // Arrange
            var sut = CreateEfRepository();
            CreateThreeInstructionsInDb(sut);

            // Act
            var result = sut.Get().ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("No1", result.Single(n => n.Id == 1).Name);

            sut.Dispose();
        }

        [TestMethod]
        public void TestShouldGetEntities()
        {
            // Arrange
            var sut = CreateEfRepository();
            CreateThreeInstructionsInDb(sut);

            // Act
            var result = sut.Get(n => n.Id == 2).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("No2", result.Single().Name);

            sut.Dispose();
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreNone()
        {
            // Arrange
            var sut = CreateEfRepository();

            // Act
            var result = sut.Insert(SampleInstructions.CreateInstruction());

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("FirstInstruction", result.Name);

            sut.Dispose();
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var sut = CreateEfRepository();
            CreateThreeInstructionsInDb(sut);

            // Act
            var result = sut.Insert(SampleInstructions.CreateInstruction());

            // Assert
            Assert.AreEqual(4, result.Id);
            Assert.AreEqual("FirstInstruction", result.Name);

            sut.Dispose();
        }

        private static EfRepository<IInstruction, Instruction> CreateEfRepository()
        {
            var options = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ExampleContext(options);
            context.Database.EnsureDeleted();

            return new EfRepository<IInstruction, Instruction>(context);
        }

        private static void CreateThreeInstructionsInDb(EfRepository<IInstruction, Instruction> sut)
        {
            sut.Insert(SampleInstructions.CreateInstruction(0, "No1", "Desc1"));
            sut.Insert(SampleInstructions.CreateInstruction(0, "No2", "Desc2"));
            sut.Insert(SampleInstructions.CreateInstruction(0, "No3", "Desc3"));
        }
    }
}