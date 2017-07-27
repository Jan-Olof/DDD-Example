using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using InfrastructureLayer.Factories;

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
        /// Insert a person.
        /// </summary>
        public Person AddPerson(Person person)
        {
            var personDao = PersonFactory.CreatePersonDao(person);

            personDao = Insert(personDao);

            return PersonFactory.CreatePerson(personDao);
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        public Product AddProduct(Product product)
        {
            return Insert(product);
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
        /// Delete a person.
        /// </summary>
        public void RemovePerson(int id)
        {
            Delete<Person>(id);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void RemoveProduct(int id)
        {
            Delete<Product>(id);
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