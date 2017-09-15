using DomainLayer.Models;
using System;
using System.Collections.Generic;

namespace ApplicationLayer.Infrastructure
{
    public interface IQueries : IDisposable
    {
        /// <summary>
        /// Get person from primary key.
        /// </summary>
        Person GetPerson(int id);

        /// <summary>
        /// Get all persons.
        /// </summary>
        List<Person> GetPersons();

        /// <summary>
        /// Get or search persons from name.
        /// </summary>
        List<Person> GetPersons(string name, bool isSearch = false);

        /// <summary>
        /// Get product from primary key.
        /// </summary>
        Product GetProduct(int id);

        /// <summary>
        /// Get all products.
        /// </summary>
        List<Product> GetProducts();

        /// <summary>
        /// Get products from name.
        /// </summary>
        List<Product> GetProducts(string name, bool isSearch = false);
    }
}