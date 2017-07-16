using System.Linq;
using ApplicationLayer.Interactors;
using DomainLayerTests.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Utilities.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces;
using DomainLayer.Models;

// ReSharper disable UnusedMember.Global

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class ProductInteractorTests
    {
        private ILogger<ProductInteractor> _logger;
        private IProduct _model;
        private IRepository<Product> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<Product>>();
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
            _repository.Insert(SampleProducts.CreateProduct())
                .ReturnsForAnyArgs(SampleProducts.CreateProduct(1));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Create(SampleProducts.CreateProduct());

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            _repository.Get()
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

            _repository.Get(i => i.Id == 3)
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Get(3);

            // Assert
            Assert.AreEqual("ThirdProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromIdAndFindsDuplicates()
        {
            // Arrange
            _model.Get(3)
                .Returns(i => i.Id == 3);

            _repository.Get(i => i.Id == 3)
                .ReturnsForAnyArgs(SampleProducts.CreateProductsDuplicate());

            var sut = CreateProductInteractor();

            // Act and assert
            Assert.ThrowsException<TooManyFoundException>(() => sut.Get(3));
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange
            _model.Get("ThirdProduct")
                .Returns(i => i.Name == "ThirdProduct");

            _repository.Get(i => i.Name == "ThirdProduct")
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.Get("ThirdProduct");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldUpdateProductFromId()
        {
            // Arrange
            _model.Get(3).Returns(i => i.Id == 3);

            _repository.Update(SampleProducts.CreateProduct());

            var sut = CreateProductInteractor();

            // Act
            sut.Update(SampleProducts.CreateProduct(3), 3);

            // Assert
            Assert.IsTrue(true);
        }

        private ProductInteractor CreateProductInteractor()
        {
            return new ProductInteractor(_repository, _model, _logger);
        }
    }
}