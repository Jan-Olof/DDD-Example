using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace ApplicationLayer.Interfaces.Infrastructure
{
    /// <summary>
    /// The domain repository interface. Handles CRUD operations.
    /// </summary>
    public interface IDomainRepository : IDisposable
    {
        /// <summary>
        /// Insert a person.
        /// </summary>
        Person AddPerson(Person person);

        /// <summary>
        /// Insert a product.
        /// </summary>
        Product AddProduct(Product product);

        /// <summary>
        /// Get person from primary key.
        /// </summary>
        Person GetPerson(int id);

        /// <summary>
        /// Get all persons.
        /// </summary>
        IEnumerable<Person> GetPersons();

        /// <summary>
        /// Get persons based on a condition.
        /// </summary>
        IEnumerable<Person> GetPersons(Expression<Func<Person, bool>> condition);

        /// <summary>
        /// Get product from primary key.
        /// </summary>
        Product GetProduct(int id);

        /// <summary>
        /// Get all products.
        /// </summary>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> condition);

        /// <summary>
        /// Delete a person.
        /// </summary>
        void RemovePerson(int id);

        /// <summary>
        /// Delete a product.
        /// </summary>
        void RemoveProduct(int id);

        /// <summary>
        /// Update a person.
        /// </summary>
        void UpdatePerson(Person person);

        /// <summary>
        /// Update a product.
        /// </summary>
        void UpdateProduct(Product product);
    }
}