// ReSharper disable UnusedMember.Global

using System;
using System.Linq;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using InfrastructureLayerTests.TestObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using static InfrastructureLayerTests.TestObjects.TestFactory;

namespace InfrastructureLayerTests.DataAccess.Repositories
{
    [TestClass]
    public class EfDomainRepositoryTests
    {
        private ILogger<EfDomainRepository> _logger;
        private DbContextOptions<ExampleContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _logger = Substitute.For<ILogger<EfDomainRepository>>();
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

                var person = sut.GetPersons(p => p.FirstName == "First").Single();

                // Act
                sut.DeletePerson(person.Id);
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

                var product = sut.GetProducts(p => p.Name == "No2").Single();

                // Act
                sut.DeleteProduct(product.Id);
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
        public void TestShouldGetAllPersons()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

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
                var sut = CreateEfRepository(context);

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
                var sut = CreateEfRepository(context);

                var person = sut.GetPersons(p => p.FirstName == "First").Single();

                // Act
                var result = sut.GetPerson(person.Id);

                // Assert
                Assert.AreEqual("First Person", result.Name);
            }
        }

        [TestMethod]
        public void TestShouldGetPersons()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetPersons(p => p.FirstName == "Second").ToList();

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
                var sut = CreateEfRepository(context);

                var product = sut.GetProducts(p => p.Name == "No1").Single();

                // Act
                var result = sut.GetProduct(product.Id);

                // Assert
                Assert.AreEqual("No1", result.Name);
            }
        }

        [TestMethod]
        public void TestShouldGetProducts()
        {
            // Arrange
            SeedDatabase(_options);

            using (var context = new ExampleContext(_options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetProducts(p => p.Name == "No2").ToList();

                // Assert
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("No2", result.Single().Name);
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
                var result = sut.InsertPerson(SamplePersons.CreatePerson());

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
                var result = sut.InsertPerson(SamplePersons.CreatePerson(0, "Fourth", "Human"));

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
                var result = sut.InsertProduct(SampleProducts.CreateProduct());

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
                var result = sut.InsertProduct(SampleProducts.CreateProduct());

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

                var person = sut.GetPersons(p => p.FirstName == "Second").Single();
                id = person.Id;

                // Act
                sut.UpdatePerson(SamplePersons.CreatePerson(id, "Updated", "Human"));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Persons.ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated Human", result.Single(p => p.Id == id).Name);
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

                int id = sut.GetProducts(new Product().Get("No2")).Single().Id;

                // Act
                sut.UpdateProduct(SampleProducts.CreateProduct(id, "No2", "Updated description."));
            }

            // Assert
            using (var context = new ExampleContext(_options))
            {
                var result = context.Products.ToList();

                Assert.AreEqual(3, result.Count);
                Assert.AreEqual("Updated description.", result.Single(p => p.Name == "No2").Description);
            }
        }

        private EfDomainRepository CreateEfRepository(ExampleContext context)
        {
            return new EfDomainRepository(context, _logger);
        }
    }
}