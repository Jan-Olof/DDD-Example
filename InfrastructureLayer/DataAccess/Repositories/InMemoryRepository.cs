﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// A simple in memory repository.
    /// </summary>
    public class InMemoryRepository : IRepository<Product> // TODO: Add person.
    {
        private readonly IFileHandler<IList<Product>> _fileHandler;
        private IList<Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository(IFileHandler<IList<Product>> fileHandler)
        {
            _products = new List<Product>();
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void Delete(Product product)
        {
            Delete(product.Id);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void Delete(int id)
        {
            var item = _products.SingleOrDefault(e => e.Id == id);

            int index = _products.IndexOf(item);
            _products.RemoveAt(index);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _products = new List<Product>();
        }

        /// <summary>
        /// Fill the data set with data from the data store.
        /// </summary>
        public void FillDataSet()
        {
            _products = _fileHandler.Read();
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public IEnumerable<Product> Get(Expression<Func<Product, bool>> condition)
        {
            return _products.Where(condition.Compile());
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        public Product Insert(Product product)
        {
            product.Id = GetNextId();
            _products.Add(product);
            return product;
        }

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        public void PersistData()
        {
            _fileHandler.Write(_products);
        }

        /// <summary>
        /// Update a product. This is based on a condition defining how to find the object.
        /// </summary>
        public void Update(Product product)
        {
            var toUpdate = _products.SingleOrDefault(product.Get(product.Id).Compile());

            product.MapUpdate(product, toUpdate);
        }

        /// <summary>
        /// Get the next (unused) id.
        /// </summary>
        private int GetNextId()
        {
            if (_products?.Any() == false)
            {
                return 1;
            }

            return _products.Max(n => n.Id) + 1;
        }
    }
}