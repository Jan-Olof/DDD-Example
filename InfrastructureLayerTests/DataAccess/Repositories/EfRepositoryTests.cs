// ReSharper disable UnusedMember.Global

using System;
using System.Linq;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class EfRepositoryTests
    {
        private ILogger<EfRepository<Product>> _logger;
        private DbContextOptions<ExampleContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _logger = Substitute.For<ILogger<EfRepository<Product>>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldDeleteEntityWhenThereAreSomeAlready()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                var instruction = sut.Get(i => i.Name == "No2").Single();

                // Act
                sut.Delete(instruction);
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Instructions.ToList();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("Desc1", result.Single(i => i.Name == "No1").Description);
                Assert.AreEqual("Desc3", result.Single(i => i.Name == "No3").Description);
            }
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
                var result = sut.Insert(SampleProducts.CreateProduct());

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
                var result = sut.Insert(SampleProducts.CreateProduct());

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

        [TestMethod]
        public void TestShouldUpdateEntity()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                sut.Update(SampleProducts.CreateProduct(2, "No2", "Updated description."), i => i.Name == "No2");
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Instructions.ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated description.", result.Single(i => i.Name == "No2").Description);
            }
        }

        private static void SeedDatabase(DbContextOptions<ExampleContext> options)
        {
            using (var context = new ExampleContext(options))
            {
                context.Database.EnsureDeleted();
                context.Instructions.Add(SampleProducts.CreateProduct(0, "No1", "Desc1"));
                context.Instructions.Add(SampleProducts.CreateProduct(0, "No2", "Desc2"));
                context.Instructions.Add(SampleProducts.CreateProduct(0, "No3", "Desc3"));
                context.SaveChanges();
            }
        }

        private EfRepository<Product> CreateEfRepository(ExampleContext context)
        {
            return new EfRepository<Product>(context, new Product(), _logger);
        }
    }
}