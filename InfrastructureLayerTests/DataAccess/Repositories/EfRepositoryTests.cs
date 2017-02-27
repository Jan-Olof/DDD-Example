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
        private DbContextOptions<ExampleContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetAllEntities()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Get().ToList();

                // Assert
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("No1", result.Single(n => n.Description == "Desc1").Name);
            }
        }

        [TestMethod]
        public void TestShouldGetEntities()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Get(n => n.Name == "No2").ToList();

                // Assert
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("No2", result.Single().Name);
            }
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreNone()
        {
            // Arrange
            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Insert(SampleInstructions.CreateInstruction());

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("FirstInstruction", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Instructions.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("FirstInstruction", result.Single().Name);
            }
        }

        [TestMethod]
        public void TestShouldInsertEntityWhenThereAreSomeAlready()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Insert(SampleInstructions.CreateInstruction());

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("FirstInstruction", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Instructions.ToList();

                Assert.AreEqual(4, result.Count);
                Assert.AreEqual("This is the first instruction.", result.Single(i => i.Name == "FirstInstruction").Description);
            }
        }

        private static EfRepository<IInstruction, Instruction> CreateEfRepository(ExampleContext context)
        {
            return new EfRepository<IInstruction, Instruction>(context);
        }

        private static void SeedDatabase(DbContextOptions<ExampleContext> options)
        {
            using (var context = new ExampleContext(options))
            {
                context.Database.EnsureDeleted();
                context.Instructions.Add((Instruction)SampleInstructions.CreateInstruction(0, "No1", "Desc1"));
                context.Instructions.Add((Instruction)SampleInstructions.CreateInstruction(0, "No2", "Desc2"));
                context.Instructions.Add((Instruction)SampleInstructions.CreateInstruction(0, "No3", "Desc3"));
                context.SaveChanges();
            }
        }
    }
}