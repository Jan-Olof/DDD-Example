using ApplicationLayer.Infrastructure;
using ApplicationLayer.Products;
using DomainLayer.Enums;
using DomainLayer.Exceptions;
using DomainLayerTests.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;

namespace ApplicationLayerTests.Services
{
    [TestClass]
    public class ProductInteractorTests
    {
        private ILogger<ProductInteractor> _logger;
        private IQueries _queries;
        private ICommands _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<ICommands>();
            _queries = Substitute.For<IQueries>();
            _logger = Substitute.For<ILogger<ProductInteractor>>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldAddPersonToProduct()
        {
            // Arrange
            _queries.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            _queries.GetProduct(1)
                .Returns(SampleProducts.CreateProduct(1));

            _repository.UpdateProduct(SampleProducts.CreateProduct(1));

            var sut = CreateProductInteractor();

            // Act
            sut.AddPersonToProduct(1, 1, Role.Actor);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestShouldAddPersonToProductAndNotFindPerson()
        {
            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.AddPersonToProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldAddPersonToProductAndNotFindProduct()
        {
            _queries.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.AddPersonToProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldAddPersonToProductAndPersonAlreadyExist()
        {
            _queries.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            _queries.GetProduct(1)
                .Returns(SampleProducts.CreateProductWithPersons(1, "", "", 1));

            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<TooManyFoundException>(() => sut.AddPersonToProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldCreateProduct()
        {
            // Arrange
            _repository.AddProduct(SampleProducts.CreateProduct())
                .ReturnsForAnyArgs(SampleProducts.CreateProduct(1));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.CreateProduct("FirstProduct", "This is the first product.");

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("This is the first product.", result.Description);
        }

        [TestMethod]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            _repository.RemoveProduct(3);

            var sut = CreateProductInteractor();

            // Act
            sut.DeleteProduct(3);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestShouldGetAllProducts()
        {
            // Arrange
            _queries.GetProducts()
                .Returns(SampleProducts.CreateProducts());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.GetProducts();

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            _queries.GetProduct(3)
                .ReturnsForAnyArgs(SampleProducts.CreateProduct(3, "ThirdProduct"));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.GetProduct(3);

            // Assert
            Assert.AreEqual("ThirdProduct", result.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromName()
        {
            // Arrange
            _queries.GetProducts("ThirdProduct")
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.GetProducts("ThirdProduct");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldRemovePersonFromProduct()
        {
            // Arrange
            _queries.GetProduct(1)
                .Returns(SampleProducts.CreateProductWithPersons(1, "", "", 1));

            _repository.UpdateProduct(SampleProducts.CreateProductWithPersons(1, "", "", 1));

            var sut = CreateProductInteractor();

            // Act
            sut.RemovePersonFromProduct(1, 1, Role.Actor);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestShouldRemovePersonFromProductAndNotFindPersonInProduct()
        {
            _queries.GetProduct(1)
                .Returns(SampleProducts.CreateProduct(1));

            _repository.UpdateProduct(SampleProducts.CreateProduct(1));

            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.RemovePersonFromProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldRemovePersonFromProductAndNotFindProduct()
        {
            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.RemovePersonFromProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldSearchProductFromName()
        {
            // Arrange
            _queries.GetProducts("Third", true)
                .ReturnsForAnyArgs(SampleProducts.CreateProducts3());

            var sut = CreateProductInteractor();

            // Act
            var result = sut.SearchProducts("Third");

            // Assert
            Assert.AreEqual(3, result.Single().Id);
        }

        [TestMethod]
        public void TestShouldUpdateProductFromId()
        {
            // Arrange
            _repository.UpdateProduct(SampleProducts.CreateProduct(3));
            _queries.GetProduct(3).ReturnsForAnyArgs(SampleProducts.CreateProduct(3));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.UpdateProduct(3, "FirstProduct", "This is the first product.");

            // Assert
            Assert.AreEqual("FirstProduct", result.Name);
        }

        private ProductInteractor CreateProductInteractor()
            => new ProductInteractor(_queries, _repository, _logger);
    }
}