using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer.Interfaces.Interactors;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

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
        public void TestShouldGetProducts()
        {
            // Arrange
            var sut = CreateProductController();

            // Act
            var result = sut.GetProducts();

            // Assert
            var okResult = (OkObjectResult)result; //TODO: HERE!
        }

        private ProductController CreateProductController()
        {
            return new ProductController(_productInteractor);
        }
    }
}