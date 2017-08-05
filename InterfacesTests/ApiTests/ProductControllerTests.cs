using System.Collections.Generic;
using System.Linq;
using System.Net;
using ApplicationLayer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer.Interfaces.Interactors;
using API.Controllers;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InterfacesTests.ApiTests
{
    [TestClass]
    public class ProductControllerTests
    {
        private IProductInteractor _productInteractor;

        [TestInitialize]
        public void SetUp()
        {
            _productInteractor = Substitute.For<IProductInteractor>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            _productInteractor.GetProduct(1).Returns(SampleProducts.CreateProduct(1));

            var sut = CreateProductController();

            // Act
            var result = sut.GetProduct(1);

            // Assert
            var okResult = (OkObjectResult)result;
            var value = (Product)okResult.Value;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual("FirstProduct", value.Name);
        }

        [TestMethod]
        public void TestShouldGetProductFromIdAndFindNone()
        {
            // Arrange
            _productInteractor.GetProduct(1).Throws(new NotFoundException());

            var sut = CreateProductController();

            // Act
            var result = sut.GetProduct(1);

            // Assert
            var notFoundResult = (NotFoundResult)result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void TestShouldGetProductFromIdAndFindNull()
        {
            // Arrange
            var sut = CreateProductController();

            // Act
            var result = sut.GetProduct(1);

            // Assert
            var notFoundResult = (NotFoundResult)result;

            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void TestShouldGetProducts()
        {
            // Arrange
            _productInteractor.GetProducts().Returns(SampleProducts.CreateProducts4());

            var sut = CreateProductController();

            // Act
            var result = sut.GetProducts();

            // Assert
            var okResult = (OkObjectResult)result;
            var value = (List<Product>)okResult.Value;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(4, value.Count);
            Assert.AreEqual("ThirdProduct", value.Single(p => p.Id == 3).Name);
        }

        [TestMethod]
        public void TestShouldGetProductsFromName()
        {
            // Arrange
            _productInteractor.SearchProducts("Product").Returns(SampleProducts.CreateProducts4());

            var sut = CreateProductController();

            // Act
            var result = sut.GetProducts("Product");

            // Assert
            var okResult = (OkObjectResult)result;
            var value = (List<Product>)okResult.Value;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(4, value.Count);
            Assert.AreEqual("ThirdProduct", value.Single(p => p.Id == 3).Name);
        }

        [TestMethod]
        public void TestShouldGetProductsFromNameAndFindsNone()
        {
            // Arrange
            _productInteractor.SearchProducts("Persons").Returns(SampleProducts.CreateProductsEmpty());

            var sut = CreateProductController();

            // Act
            var result = sut.GetProducts("Product");

            // Assert
            var noContentResult = (NoContentResult)result;

            Assert.AreEqual((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        private ProductController CreateProductController()
        {
            return new ProductController(_productInteractor);
        }
    }
}