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
        Person GetPerson(int id, bool includeMembers = true);

        /// <summary>
        /// Get all persons.
        /// </summary>
        List<Person> GetPersons(bool includeMembers = true);

        /// <summary>
        /// Get or search persons from name.
        /// </summary>
        List<Person> GetPersons(string name, bool isSearch = false, bool includeMembers = true);

        /// <summary>
        /// Get product from primary key.
        /// </summary>
        Product GetProduct(int id, bool includeMembers = true);

        /// <summary>
        /// Get all products.
        /// </summary>
        List<Product> GetProducts(bool includeMembers = true);

        /// <summary>
        /// Get products from name.
        /// </summary>
        List<Product> GetProducts(string name, bool isSearch = false, bool includeMembers = true);
    }
}