using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayer.Configure;
using InfrastructureLayer.Files;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace InfrastructureLayerTests.Files
{
    [TestClass]
    public class FileHandlerTests
    {
        private const string ProductsFileName = @"..\..\..\Products.json";
        private IOptions<Datafile> _dataFile;

        [TestInitialize]
        public void SetUp()
        {
            _dataFile = Substitute.For<IOptions<Datafile>>();
            RestoreFileContent();
        }

        [TestCleanup]
        public void TearDown()
        {
            RestoreFileContent();
        }

        [TestMethod]
        public void TestShouldReadAllProducts()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = ProductsFileName });

            var sut = CreateFileHandler();

            // Act
            var result = sut.Read();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("SecondProduct", result.Single(r => r.Id == 2).Name);
        }

        [TestMethod]
        public void TestShouldWriteProductsToFile()
        {
            // Arrange
            _dataFile.Value.Returns(new Datafile { FileName = ProductsFileName });

            var sut = CreateFileHandler();

            var products = sut.Read();
            products.Add(SampleProducts.CreateProduct(3, "ThirdProduct", "This is the third product."));

            // Act
            sut.Write(products);

            // Assert
            var result = sut.Read();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("SecondProduct", result.Single(r => r.Id == 2).Name);
            Assert.AreEqual("ThirdProduct", result.Single(r => r.Id == 3).Name);
        }

        private IFileHandler<IList<Product>> CreateFileHandler()
        {
            return new FileHandler<IList<Product>>(_dataFile, new JsonSerialization());
        }

        private void RestoreFileContent()
        {
            _dataFile.Value.Returns(new Datafile { FileName = ProductsFileName });
            var sut = CreateFileHandler();
            sut.Write(SampleProducts.CreateProducts2());
        }
    }
}