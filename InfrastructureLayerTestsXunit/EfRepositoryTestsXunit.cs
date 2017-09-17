using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Linq;
using Xunit;
using static InfrastructureLayerTests.TestObjects.TestFactory;

namespace InfrastructureLayerTestsXunit
{
    public class EfRepositoryTestsXunit
    {
        private readonly ILogger<EfCommandsRepository> _logger;

        public EfRepositoryTestsXunit()
        {
            _logger = Substitute.For<ILogger<EfCommandsRepository>>();
        }

        [Fact]
        public void TestShouldDeletePerson()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var person = sut.GetPersons("Second", true).Single();

                // Act
                sut.RemovePerson(person.Id);
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Persons.ToList();

                Assert.Equal(2, result.Count);
                Assert.Equal("First Person", result.Single(p => p.FirstName == "First").Name);
                Assert.Equal("Third Person", result.Single(p => p.FirstName == "Third").Name);
            }
        }

        [Fact]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var product = sut.GetProducts("No2").Single();

                // Act
                sut.RemoveProduct(product.Id);
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(2, result.Count);
                Assert.Equal("Desc1", result.Single(p => p.Name == "No1").Description);
                Assert.Equal("Desc3", result.Single(p => p.Name == "No3").Description);
            }
        }

        [Fact]
        public void TestShouldGetAllPersons()
        {
            // Arrange
            var options = SetDbContextOptions();

            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetPersons().ToList();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Equal("First Person", result.Single(p => p.FirstName == "First").Name);
            }
        }

        [Fact]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetProducts().ToList();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Equal("No1", result.Single(p => p.Description == "Desc1").Name);
            }
        }

        [Fact]
        public void TestShouldGetPerson()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var person = sut.GetPersons("First", true).Single();

                // Act
                var result = sut.GetPerson(person.Id);

                // Assert
                Assert.Equal("First Person", result.Name);
            }
        }

        [Fact]
        public void TestShouldGetPersons()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetPersons("Second", true).ToList();

                // Assert
                Assert.Equal(1, result.Count);
                Assert.Equal("Second Person", result.Single().Name);
            }
        }

        [Fact]
        public void TestShouldGetProduct()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var product = sut.GetProducts("No1").Single();

                // Act
                var result = sut.GetProduct(product.Id);

                // Assert
                Assert.Equal("No1", result.Name);
            }
        }

        [Fact]
        public void TestShouldGetProducts()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.GetProducts("No2").ToList();

                // Assert
                Assert.Equal(1, result.Count);
                Assert.Equal("No2", result.Single().Name);
            }
        }

        [Fact]
        public void TestShouldInsertPersontWhenThereAreSomeAlready()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddPerson(SamplePersons.CreatePerson(0, "Fourth", "Human"));

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("Fourth Human", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Persons.ToList();

                Assert.Equal(4, result.Count);
                Assert.Equal("Fourth Human", result.Single(p => p.FirstName == "Fourth").Name);
            }
        }

        [Fact]
        public void TestShouldInsertPersonWhenThereAreNone()
        {
            // Arrange
            var options = SetDbContextOptions();
            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddPerson(SamplePersons.CreatePerson());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("First Person", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Persons.ToList();

                Assert.Equal(1, result.Count);
                Assert.Equal("First Person", result.Single().Name);
            }
        }

        [Fact]
        public void TestShouldInsertProductWhenThereAreNone()
        {
            // Arrange
            var options = SetDbContextOptions();
            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddProduct(SampleProducts.CreateProduct());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("FirstProduct", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(1, result.Count);
                Assert.Equal("FirstProduct", result.Single().Name);
            }
        }

        [Fact]
        public void TestShouldInsertProductWhenThereAreSomeAlready()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.AddProduct(SampleProducts.CreateProduct());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("FirstProduct", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(4, result.Count);
                Assert.Equal("This is the first product.", result.Single(p => p.Name == "FirstProduct").Description);
            }
        }

        [Fact]
        public void TestShouldUpdatePerson()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            int id;
            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                id = sut.GetPersons("Second", true).Single().Id;

                // Act
                sut.UpdatePerson(SamplePersons.CreatePerson(id, "Updated", "Human"));
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Persons.ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal("Updated Human", result.Single(p => p.Id == id).Name);
            }
        }

        [Fact]
        public void TestShouldUpdateProduct()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                int id = sut.GetProducts("No2").Single().Id;

                // Act
                sut.UpdateProduct(SampleProducts.CreateProduct(id, "No2", "Updated description."));
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal("Updated description.", result.Single(p => p.Name == "No2").Description);
            }
        }

        private static DbContextOptions<ExampleContext> SetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private EfCommandsRepository CreateEfRepository(ExampleContext context)
        {
            return new EfCommandsRepository(context, _logger);
        }
    }
}