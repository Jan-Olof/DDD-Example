using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Interfaces;
using InfrastructureLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
            => _productInteractor = productInteractor ?? throw new ArgumentNullException(nameof(productInteractor));

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="product">An object containing the new product to create.</param>
        [ProducesResponseType(typeof(IProductDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductCreate product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var createdProduct = _productInteractor.CreateProduct(product.Name, product.Description);

            return CreatedAtRoute("GetProduct", new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id">The id of the product to delete.</param>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productInteractor.DeleteProduct(id);

            return new NoContentResult();
        }

        /// <summary>
        /// Get one product from id.
        /// </summary>
        /// <param name="id">The id of the product to get.</param>
        [ProducesResponseType(typeof(IProductDto), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(List<IProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public IActionResult GetProducts()
            => Ok(_productInteractor.GetProducts());

        /// <summary>
        /// Search products for a certain name.
        /// </summary>
        /// <param name="name">Search for everything that contains this string.</param>
        /// <returns>All products that have a name that contains the search string.</returns>
        [ProducesResponseType(typeof(List<IProductDto>), (int)HttpStatusCode.OK)]
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

        /// <summary>
        /// Update a customer.
        /// </summary>
        /// <param name="product">An object containing the new values of the product to update.</param>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(HttpError), (int)HttpStatusCode.InternalServerError)]
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] ProductUpdate product)
        {
            _productInteractor.UpdateProduct(product.Id, product.Name, product.Description);

            return new NoContentResult();
        }
    }
}