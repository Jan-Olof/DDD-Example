using System.Linq;
using ApplicationLayer.Interactors;
using DomainLayerTests.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces;

// ReSharper disable UnusedMember.Global

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class ProductInteractorTests
    {
        private ILogger<ProductInteractor> _logger;
        private IProduct _model;
        private IDomainRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IDomainRepository>();
            _model = Substitute.For<IProduct>();
            _logger = Substitute.For<ILogger<ProductInteractor>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldCreateProduct()
        {
            // Arrange
            _repository.InsertProduct(SampleProducts.CreateProduct())
                .ReturnsForAnyArgs(SampleProducts.CreateProduct(1));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Create("FirstProduct", "This is the first product.");

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("This is the first product.", result.Description);
        }

        [TestMethod]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            _repository.DeleteProduct(3);

            var sut = CreateProductInteractor();

            // Act
            sut.Delete(3);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            _repository.GetProducts()
                .Returns(SampleProducts.CreateProducts());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Get();

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            _model.Get(3)
                .Returns(i => i.Id == 3);

            _repository.GetProduct(3)
                .Returns(SampleProducts.CreateProduct(3, "ThirdProduct"));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Get(3);

            // Assert
            Assert.AreEqual("ThirdProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange
            _model.Get("ThirdProduct")
                .Returns(i => i.Name == "ThirdProduct");

            _repository.GetProducts(i => i.Name == "ThirdProduct")
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Get("ThirdProduct");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldSearchProductFromName()
        {
            // Arrange
            _model.Search("Third")
                .Returns(i => i.Name == "Third");

            _repository.GetProducts(i => i.Name.Contains("Third"))
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Search("Third");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldUpdateProductFromId()
        {
            // Arrange
            _repository.UpdateProduct(SampleProducts.CreateProduct(3));
            _repository.GetProduct(3).Returns(SampleProducts.CreateProduct(3));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Update(3, "FirstProduct", "This is the first product.");

            // Assert
            Assert.AreEqual("FirstProduct", result.Name);
        }

        private ProductInteractor CreateProductInteractor()
        {
            return new ProductInteractor(_repository, _model, _logger);
        }
    }
}