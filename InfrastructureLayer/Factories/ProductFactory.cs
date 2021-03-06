﻿using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
using InfrastructureLayer.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureLayer.Factories
{
    /// <summary>
    /// Builds products.
    /// </summary>
    public static class ProductFactory
    {
        /// <summary>
        /// Build a Product from a ProductDao.
        /// </summary>
        public static Product CreateProduct(ProductDao productDao)
            => new Product
            {
                Id = productDao.Id,
                Name = productDao.Name,
                Description = productDao.Description,
                Persons = CreatePersonsInProduct(productDao.ProductPersons)
            };

        /// <summary>
        /// Build a ProductDao from a Product.
        /// </summary>
        public static ProductDao CreateProductDao(Product product)
            => new ProductDao
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ProductPersons = CreateProductPersons(product.Persons, product.Id)
            };

        /// <summary>
        /// Build a ProductDto from a Product.
        /// </summary>
        public static ProductDto CreateProductDto(Product product)
            => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Persons = product.Persons
            };

        private static List<PersonInProduct> CreatePersonsInProduct(IEnumerable<ProductPerson> productPersons)
            => productPersons
                .Select(ProductPersonFactory.CreatePersonInProduct)
                .ToList();

        private static List<ProductPerson> CreateProductPersons(IEnumerable<PersonInProduct> personsInProduct, int productId)
            => personsInProduct
                .Select(p => ProductPersonFactory.CreateProductPerson(p, productId))
                .ToList();
    }
}