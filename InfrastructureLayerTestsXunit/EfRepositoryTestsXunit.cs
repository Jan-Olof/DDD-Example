using System;
using System.Linq;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace InfrastructureLayerTestsXunit
{
    public class EfRepositoryTestsXunit
    {
        private readonly ILogger<EfRepository<Product>> _logger;

        public EfRepositoryTestsXunit()
        {
            _logger = Substitute.For<ILogger<EfRepository<Product>>>();
        }

        [Fact]
        public void TestShouldDeleteEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var product = sut.Get(i => i.Name == "No2").Single();

                // Act
                sut.Delete(product);
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(2, result.Count);
                Assert.Equal("Desc1", result.Single(i => i.Name == "No1").Description);
                Assert.Equal("Desc3", result.Single(i => i.Name == "No3").Description);
            }
        }

        [Fact]
        public void TestShouldGetAllEntities()
        {
            // Arrange
            var options = SetDbContextOptions();

            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Get().ToList();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Equal("No1", result.Single(n => n.Description == "Desc1").Name);
            }
        }

        [Fact]
        public void TestShouldGetEntities()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Get(n => n.Name == "No2").ToList();

                // Assert
                Assert.Equal(1, result.Count);
                Assert.Equal("No2", result.Single().Name);
            }
        }

        [Fact]
        public void TestShouldInsertEntityWhenThereAreNone()
        {
            // Arrange
            var options = SetDbContextOptions();
            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Insert(SampleProducts.CreateProduct());

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
        public void TestShouldInsertEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                var result = sut.Insert(SampleProducts.CreateProduct());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("FirstProduct", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(4, result.Count);
                Assert.Equal("This is the first product.", result.Single(i => i.Name == "FirstProduct").Description);
            }
        }

        [Fact]
        public void TestShouldUpdateEntity()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                // Act
                sut.Update(SampleProducts.CreateProduct(2, "No2", "Updated description."), i => i.Name == "No2");
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Products.ToList();

                Assert.Equal(3, result.Count);
                Assert.Equal("Updated description.", result.Single(i => i.Name == "No2").Description);
            }
        }

        private static void SeedDatabase(DbContextOptions<ExampleContext> options)
        {
            using (var context = new ExampleContext(options))
            {
                context.Database.EnsureDeleted();
                context.Products.Add(SampleProducts.CreateProduct(0, "No1", "Desc1"));
                context.Products.Add(SampleProducts.CreateProduct(0, "No2", "Desc2"));
                context.Products.Add(SampleProducts.CreateProduct(0, "No3", "Desc3"));
                context.SaveChanges();
            }
        }

        private static DbContextOptions<ExampleContext> SetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private EfRepository<Product> CreateEfRepository(ExampleContext context)
        {
            return new EfRepository<Product>(context, new Product(), _logger);
        }
    }
}