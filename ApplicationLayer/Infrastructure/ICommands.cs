using DomainLayer.Models;
using System;

namespace ApplicationLayer.Infrastructure
{
    /// <summary>
    /// The commands interface. Handles CUD operations.
    /// </summary>
    public interface ICommands : IDisposable
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