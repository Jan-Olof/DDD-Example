using ApplicationLayer.EventLogging;
using ApplicationLayer.Factories;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// A simple in memory repository.
    /// </summary>
    public class InMemoryRepository : IDomainRepository // TODO: Fix NotImplementedException and remove not used!.
    {
        private readonly IFileHandler<IList<Product>> _fileHandler;
        private readonly ILogger _logger;
        private IList<Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository(IFileHandler<IList<Product>> fileHandler, ILogger<InMemoryRepository> logger)
        {
            _products = new List<Product>();
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Person AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        public Product AddProduct(Product product)
        {
            product.Id = GetNextId();
            _products.Add(product);

            _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<Product>.CreateEventObject(product, EventType.Create)));

            return product;
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

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons(string name, bool isSearch = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons(Expression<Func<Person, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(string name, bool isSearch = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> condition)
        {
            return _products.Where(condition.Compile());
        }

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        public void PersistData()
        {
            _fileHandler.Write(_products);
        }

        public void RemovePerson(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void RemoveProduct(int id)
        {
            var item = _products.SingleOrDefault(e => e.Id == id);

            int index = _products.IndexOf(item);
            _products.RemoveAt(index);
            _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<Product>.CreateEventObject(item, EventType.Delete)));
        }

        public void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a product. This is based on a condition defining how to find the object.
        /// </summary>
        public void UpdateProduct(Product product)
        {
            var toUpdate = _products.SingleOrDefault(Product.Get(product.Id).Compile());

            product.MapUpdate(product, toUpdate);

            _logger.LogInformation(JsonConvert.SerializeObject(EventObjectFactory<Product>.CreateEventObject(product, EventType.Update)));
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