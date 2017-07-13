using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.Configure;
using InfrastructureLayer.Files;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace InfrastructureLayerTests.TestObjects
{
    public static class TestFactory
    {
        public static Datafile CreateDatafile()
        {
            return new Datafile { FileName = @"..\..\..\Products.json" };
        }

        public static IOptions<Datafile> CreateDatafileOptions()
        {
            return Options.Create(CreateDatafile());
        }

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
    }
}