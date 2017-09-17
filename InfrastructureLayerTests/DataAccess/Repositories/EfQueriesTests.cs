using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using static InfrastructureLayerTests.TestObjects.TestFactory;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class EfQueriesTests
    {
        private ILogger<EfCommandsRepository> _logger;
        private DbContextOptions<ExampleContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _logger = Substitute.For<ILogger<EfCommandsRepository>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetAllPersons()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                // Act
                var result = sut.GetPersons().ToList();

                // Assert
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("First Person", result.Single(p => p.FirstName == "First").Name);
            }
        }

        [TestMethod]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                // Act
                var result = sut.GetProducts().ToList();

                // Assert
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("No1", result.Single(p => p.Description == "Desc1").Name);
            }
        }

        [TestMethod]
        public void TestShouldGetPerson()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                var person = sut.GetPersons("First", true).Single();

                // Act
                var result = sut.GetPerson(person.Id);

                // Assert
                Assert.AreEqual("First Person", result.Name);
                Assert.AreEqual(2, result.Products.Count);
            }
        }

        [TestMethod]
        public void TestShouldGetPersons()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                // Act
                var result = sut.GetPersons("Second", true).ToList();

                // Assert
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("Second Person", result.Single().Name);
            }
        }

        [TestMethod]
        public void TestShouldGetProduct()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                var product = sut.GetProducts("No1").Single();

                // Act
                var result = sut.GetProduct(product.Id);

                // Assert
                Assert.AreEqual("No1", result.Name);
                Assert.AreEqual(1, result.Persons.Count);
            }
        }

        [TestMethod]
        public void TestShouldGetProducts()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfQueries(context);

                // Act
                var result = sut.GetProducts("No2").ToList();

                // Assert
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("No2", result.Single().Name);
            }
        }

        private EfQueries CreateEfQueries(ExampleContext context)
            => new EfQueries(context, _logger);
    }
}