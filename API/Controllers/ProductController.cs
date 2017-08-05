using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ApplicationLayer.Exceptions;
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
        /// Get one product from id.
        /// </summary>
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _productInteractor.GetProduct(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productInteractor.GetProducts());
        }

        /// <summary>
        /// Search products for a certain name.
        /// </summary>
        /// <param name="name">Search for everything that contains this string.</param>
        /// <returns>All products that have a name that contains the search string.</returns>
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpGet("name/{name}")]
        public IActionResult GetProducts(string name)
        {
            var products = _productInteractor.SearchProducts(name);

            if (products == null || !products.Any())
            {
                return new NoContentResult();
            }

            return Ok(products);
        }
    }
}