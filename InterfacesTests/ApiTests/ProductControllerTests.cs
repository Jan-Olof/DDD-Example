using API.Controllers;
using ApplicationLayer.Products;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayerTests.TestObjects;
using InfrastructureLayerTests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        public void TestShouldCreateProduct()
        {
            // Arrange
            _productInteractor.CreateProduct("FirstProduct", "This is the first product.")
                .Returns(SampleProducts.CreateProduct(1));

            var sut = CreateProductController();

            // Act
            var result = sut.CreateProduct(SampleProductDtos.CreateProductCreate());

            // Assert
            var createdResult = (CreatedAtRouteResult)result;
            var value = (Product)createdResult.Value;

            Assert.AreEqual((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.AreEqual("FirstProduct", value.Name);
        }

        [TestMethod]
        public void TestShouldCreateProductAndGetBadRequest()
        {
            // Arrange
            var sut = CreateProductController();

            // Act
            var result = sut.CreateProduct(null);

            // Assert
            var createdResult = (BadRequestResult)result;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, createdResult.StatusCode);
        }

        [TestMethod]
        public void TestShouldDeleteProduct()
        {
            // Arrange
            var sut = CreateProductController();

            // Act
            var result = sut.DeleteProduct(1);

            // Assert
            var createdResult = (NoContentResult)result;

            Assert.AreEqual((int)HttpStatusCode.NoContent, createdResult.StatusCode);
        }

        [TestMethod]
        public void TestShouldGetProductFromId()
        {
            // Arrange
            _productInteractor.GetProduct(1)
                .Returns(SampleProducts.CreateProduct(1));

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
            _productInteractor.GetProduct(1)
                .Throws(new NotFoundException());

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
            _productInteractor.GetProducts()
                .Returns(SampleProducts.CreateProducts4());

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
            _productInteractor.SearchProducts("Product")
                .Returns(SampleProducts.CreateProducts4());

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
            _productInteractor.SearchProducts("Persons")
                .Returns(SampleProducts.CreateProductsEmpty());

            var sut = CreateProductController();

            // Act
            var result = sut.GetProducts("Product");

            // Assert
            var noContentResult = (NoContentResult)result;

            Assert.AreEqual((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [TestMethod]
        public void TestShouldUpdateProduct()
        {
            // Arrange
            var sut = CreateProductController();

            // Act
            var result = sut.UpdateProduct(SampleProductDtos.CreateProductUpdate());

            // Assert
            var createdResult = (NoContentResult)result;

            Assert.AreEqual((int)HttpStatusCode.NoContent, createdResult.StatusCode);
        }

        private ProductController CreateProductController()
        {
            return new ProductController(_productInteractor);
        }
    }
}