using System;
using System.Collections.Generic;
using System.Net;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Models;
using InfrastructureLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// The product controller.
    /// </summary>
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductInteractor _productInteractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        public ProductController(IProductInteractor productInteractor)
        {
            _productInteractor = productInteractor ?? throw new ArgumentNullException(nameof(productInteractor));
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [ProducesResponseType(typeof(List<List<Product>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productInteractor.GetProducts();
            return Ok(products);
        }
    }
}