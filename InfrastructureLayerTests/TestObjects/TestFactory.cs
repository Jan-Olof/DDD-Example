using ApplicationLayer.Infrastructure;
using DomainLayer.Enums;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.Configure;
using InfrastructureLayer.DataAccess.SqlServer;
using InfrastructureLayer.Helpers.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureLayerTests.TestObjects
{
    public static class TestFactory
    {
        public static Datafile CreateDatafile()
            => new Datafile { FileName = @"..\..\..\Products.json" };

        public static IOptions<Datafile> CreateDatafileOptions()
            => Options.Create(CreateDatafile());

        public static IFileHandler<IList<Product>> CreateFileHandler()
        {
            var datafile = CreateDatafileOptions();
            return new FileHandler<IList<Product>>(datafile, new JsonSerialization());
        }

        public static void RestoreFileContent()
        {
            var sut = CreateFileHandler();
            sut.Write(SampleProducts.CreateProducts2());
        }

        public static void SeedDatabase(DbContextOptions<ExampleContext> options)
        {
            using (var context = new ExampleContext(options))
            {
                context.Database.EnsureDeleted();

                context.Products.Add(SampleProductDaos.CreateProduct(0, "No1", "Desc1"));
                context.Products.Add(SampleProductDaos.CreateProduct(0, "No2", "Desc2"));
                context.Products.Add(SampleProductDaos.CreateProduct(0, "No3", "Desc3"));
                context.Persons.Add(SamplePersonDaos.CreatePerson());
                context.Persons.Add(SamplePersonDaos.CreatePerson(0, "Second"));
                context.Persons.Add(SamplePersonDaos.CreatePerson(0, "Third"));

                context.SaveChanges();

                int prod1Id = context.Products.Single(p => p.Name == "No1").Id;
                int prod2Id = context.Products.Single(p => p.Name == "No2").Id;
                int pers1Id = context.Persons.Single(p => p.FirstName == "First").Id;
                int pers2Id = context.Persons.Single(p => p.FirstName == "Second").Id;

                context.ProductPersons.Add(SampleProductPersons.CreateProductPerson(prod1Id, pers1Id, Role.Actor));
                context.ProductPersons.Add(SampleProductPersons.CreateProductPerson(prod2Id, pers1Id, Role.Actor));
                context.ProductPersons.Add(SampleProductPersons.CreateProductPerson(prod2Id, pers2Id, Role.Director));

                context.SaveChanges();
            }
        }
    }
}