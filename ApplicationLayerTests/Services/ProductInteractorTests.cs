using System.Linq;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interactors;
using DomainLayerTests.TestObjects;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Enums;
using DomainLayer.Interfaces;

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
        public void TestShouldAddPersonToProduct()
        {
            // Arrange
            _repository.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            _repository.GetProduct(1)
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
            Assert.ThrowsException<NotFoundException>(() => sut.AddPersonToProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldAddPersonToProductAndNotFindProduct()
        {
            _repository.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<NotFoundException>(() => sut.AddPersonToProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldAddPersonToProductAndPersonAlreadyExist()
        {
            _repository.GetPerson(1)
                .Returns(SamplePersons.CreatePerson(1));

            _repository.GetProduct(1)
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
            _repository.GetProducts()
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
            _repository.GetProduct(3)
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
            _repository.GetProducts("ThirdProduct")
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
            _repository.GetProduct(1)
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
            _repository.GetProduct(1)
                .Returns(SampleProducts.CreateProduct(1));

            _repository.UpdateProduct(SampleProducts.CreateProduct(1));

            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<NotFoundException>(() => sut.RemovePersonFromProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldRemovePersonFromProductAndNotFindProduct()
        {
            // Arrange
            var sut = CreateProductInteractor();

            // Act & Assert
            Assert.ThrowsException<NotFoundException>(() => sut.RemovePersonFromProduct(1, 1, Role.Actor));
        }

        [TestMethod]
        public void TestShouldSearchProductFromName()
        {
            // Arrange
            _repository.GetProducts("Third", true)
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
            _repository.GetProduct(3).ReturnsForAnyArgs(SampleProducts.CreateProduct(3));

            var sut = CreateProductInteractor();

            // Act
            var result = sut.UpdateProduct(3, "FirstProduct", "This is the first product.");

            // Assert
            Assert.AreEqual("FirstProduct", result.Name);
        }

        private ProductInteractor CreateProductInteractor()
        {
            return new ProductInteractor(_repository, _model, _logger);
        }
    }
}