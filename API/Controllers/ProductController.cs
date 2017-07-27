using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        public IEnumerable<ProductDto> GetProduct()
        {
            return new List<ProductDto>();
        }
    }
}