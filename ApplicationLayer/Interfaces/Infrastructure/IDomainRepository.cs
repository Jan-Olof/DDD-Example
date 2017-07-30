using System;
using System.Collections.Generic;
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
        /// Get or search persons from name.
        /// </summary>
        IEnumerable<Person> GetPersons(string name, bool isSearch = false);

        /// <summary>
        /// Get product from primary key.
        /// </summary>
        Product GetProduct(int id);

        /// <summary>
        /// Get all products.
        /// </summary>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// Get products from name.
        /// </summary>
        IEnumerable<Product> GetProducts(string name, bool isSearch = false);

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