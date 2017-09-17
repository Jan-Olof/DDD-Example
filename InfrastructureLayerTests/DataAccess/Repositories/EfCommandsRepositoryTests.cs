using DomainLayer.Enums;
using DomainLayerTests.TestObjects;
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
    public class EfCommandsRepositoryTests
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
        public void TestShouldDeletePerson()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                var person = CreateEfQueries(context).GetPersons("First", true).Single();

                // Act
                sut.RemovePerson(person.Id);
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Persons.ToList();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("Second Person", result.Single(p => p.FirstName == "Second").Name);
                Assert.AreEqual("Third Person", result.Single(p => p.FirstName == "Third").Name);
            }
        }

        [TestMethod]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                var product = CreateEfQueries(context).GetProducts("No2").Single();

                // Act
                sut.RemoveProduct(product.Id);
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Products.ToList();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("Desc1", result.Single(p => p.Name == "No1").Description);
                Assert.AreEqual("Desc3", result.Single(p => p.Name == "No3").Description);
            }
        }

        [TestMethod]
        public void TestShouldInsertPersonWhenThereAreNone()
        {
            // Arrange
            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddPerson(SamplePersons.CreatePerson());

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("First Person", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Persons.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("First Person", result.Single().Name);
            }
        }

        [TestMethod]
        public void TestShouldInsertPersonWhenThereAreSomeAlready()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddPerson(SamplePersons.CreatePerson(0, "Fourth", "Human"));

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("Fourth Human", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Persons.ToList();

                Assert.AreEqual(4, result.Count);
                Assert.AreEqual("Fourth Human", result.Single(p => p.FirstName == "Fourth").Name);
            }
        }

        [TestMethod]
        public void TestShouldInsertProductWhenThereAreNone()
        {
            // Arrange
            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddProduct(SampleProducts.CreateProduct());

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("FirstProduct", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Products.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("FirstProduct", result.Single().Name);
            }
        }

        [TestMethod]
        public void TestShouldInsertProductWhenThereAreSomeAlready()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddProduct(SampleProducts.CreateProduct());

                // Assert
                Assert.IsTrue(result.Id > 0);
                Assert.AreEqual("FirstProduct", result.Name);
            }

            using (var context = new ExampleContext(_options))
            {
                var result = context.Products.ToList();

                Assert.AreEqual(4, result.Count);
                Assert.AreEqual("This is the first product.", result.Single(p => p.Name == "FirstProduct").Description);
            }
        }

        [TestMethod]
        public void TestShouldUpdatePerson()
        {
            // Arrange
            SeedDatabase(_options);

            int id;

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                var person = CreateEfQueries(context).GetPersons("Second", true).Single();
                id = person.Id;

                // Act
                sut.UpdatePerson(SamplePersons.CreatePerson(id, "Updated", "Human"));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = CreateEfQueries(context).GetPersons().ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated Human", result.Single(p => p.Id == id).Name);
                Assert.AreEqual(0, result.Single(p => p.Id == id).Products.Count);
            }
        }

        [TestMethod]
        public void TestShouldUpdatePersonWithProductRelations()
        {
            // Arrange
            SeedDatabase(_options);

            int id;

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                id = CreateEfQueries(context).GetPersons("Second", true).Single().Id;
                int productid = CreateEfQueries(context).GetProducts("No2").Single().Id;

                // Act
                sut.UpdatePerson(SamplePersons.CreatePersonWithProducts(id, "Updated", "Human", productid, Role.Producer));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = CreateEfQueries(context).GetPersons().ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated Human", result.Single(p => p.Id == id).Name);
                Assert.AreEqual(1, result.Single(p => p.Id == id).Products.Count);
                Assert.AreEqual(Role.Producer, result.Single(p => p.Id == id).Products.Single().Role);
            }
        }

        [TestMethod]
        public void TestShouldUpdateProduct()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                int id = CreateEfQueries(context).GetProducts("No2").Single().Id;

                // Act
                sut.UpdateProduct(SampleProducts.CreateProduct(id, "No2", "Updated description."));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = CreateEfQueries(context).GetProducts().ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated description.", result.Single(p => p.Name == "No2").Description);
                Assert.AreEqual(0, result.Single(p => p.Name == "No2").Persons.Count);
            }
        }

        [TestMethod]
        public void TestShouldUpdateProductWithPersonRelations()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                int id = CreateEfQueries(context).GetProducts("No2").Single().Id;
                int personid = CreateEfQueries(context).GetPersons("Second", true).Single().Id;

                // Act
                sut.UpdateProduct(SampleProducts.CreateProductWithPersons(id, "No2", "Updated description.", personid, Role.Writer));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = CreateEfQueries(context).GetProducts().ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated description.", result.Single(p => p.Name == "No2").Description);
                Assert.AreEqual(1, result.Single(p => p.Name == "No2").Persons.Count);
                Assert.AreEqual(Role.Writer, result.Single(p => p.Name == "No2").Persons.Single().Role);
            }
        }

        private EfQueries CreateEfQueries(ExampleContext context)
            => new EfQueries(context, _logger);

        private EfCommandsRepository CreateEfRepository(ExampleContext context)
            => new EfCommandsRepository(context, _logger);
    }
}