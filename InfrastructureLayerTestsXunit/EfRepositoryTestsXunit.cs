using System;
using System.Linq;
using ApplicationLayer.Interfaces.Models;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.DataAccess.Repositories;
using InfrastructureLayer.DataAccess.SqlServer;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InfrastructureLayerTestsXunit
{
    public class EfRepositoryTestsXunit
    {
        [Fact]
        public void TestShouldDeleteEntityWhenThereAreSomeAlready()
        {
            // Arrange
            var options = SetDbContextOptions();
            SeedDatabase(options);

            using (var context = new ExampleContext(options))
            {
                var sut = CreateEfRepository(context);

                var instruction = sut.Get(i => i.Name == "No2").Single();

                // Act
                sut.Delete(instruction);
            }

            // Assert
            using (var context = new ExampleContext(options))
            {
                var result = context.Instructions.ToList();

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
                var result = sut.Insert(SampleInstructions.CreateInstruction());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("FirstInstruction", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Instructions.ToList();

                Assert.Equal(1, result.Count);
                Assert.Equal("FirstInstruction", result.Single().Name);
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
                var result = sut.Insert(SampleInstructions.CreateInstruction());

                // Assert
                Assert.True(result.Id > 0);
                Assert.Equal("FirstInstruction", result.Name);
            }

            using (var context = new ExampleContext(options))
            {
                var result = context.Instructions.ToList();

                Assert.Equal(4, result.Count);
                Assert.Equal("This is the first instruction.", result.Single(i => i.Name == "FirstInstruction").Description);
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

        private static DbContextOptions<ExampleContext> SetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}