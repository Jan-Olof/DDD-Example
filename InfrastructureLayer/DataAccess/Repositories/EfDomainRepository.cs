using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Enums;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork implementation of the domain repository.
    /// </summary>
    public class EfDomainRepository : EfRepositoryBase, IDomainRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfDomainRepository"/> class.
        /// </summary>
        public EfDomainRepository(DbContext dataContext, ILogger<EfDomainRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        public void DeletePerson(int id)
        {
            Delete<Person>(id);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void DeleteProduct(int id)
        {
            Delete<Product>(id);
        }

        /// <summary>
        /// Fill the data set with data from the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void FillDataSet()
        {
        }

        /// <summary>
        /// Get person from primary key.
        /// </summary>
        public Person GetPerson(int id)
        {
            return Get<Person>(id);
        }

        /// <summary>
        /// Get all persons.
        /// </summary>
        public IEnumerable<Person> GetPersons()
        {
            return Get<Person>();
            //.Include(p => p.ProductPersons);
        }

        /// <summary>
        /// Get persons based on a condition.
        /// </summary>
        public IEnumerable<Person> GetPersons(Expression<Func<Person, bool>> condition)
        {
            return Get(condition);
            //.Include(p => p.ProductPersons);
        }

        /// <summary>
        /// Get product from primary key.
        /// </summary>
        public Product GetProduct(int id)
        {
            return Get<Product>(id);
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IEnumerable<Product> GetProducts()
        {
            return Get<Product>();
            //.Include(p => p.ProductPersons);
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> condition)
        {
            return Get(condition);
            //.Include(p => p.ProductPersons);
        }

        /// <summary>
        /// Insert a person.
        /// </summary>
        public Person InsertPerson(Person person)
        {
            return Insert(person);
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        public Product InsertProduct(Product product)
        {
            return Insert(product);
        }

        /// <summary>
        /// Persist data to the data store.
        /// EF don't support this behaviour.
        /// </summary>
        public void PersistData()
        {
        }

        /// <summary>
        /// Update a person.
        /// </summary>
        public void UpdatePerson(Person person)
        {
            Update(person);
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void UpdateProduct(Product product)
        {
            Update(product);
        }
    }
}