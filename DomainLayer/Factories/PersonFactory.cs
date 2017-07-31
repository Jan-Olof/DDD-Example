using DomainLayer.Enums;
using DomainLayer.Models;

namespace DomainLayer.Factories
{
    /// <summary>
    /// Builds persons
    /// </summary>
    public static class PersonFactory
    {
        /// <summary>
        /// Build a person.
        /// </summary>
        public static Person CreatePerson(string firstName, string lastName, int id = 0)
        {
            return new Person
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
        }

        /// <summary>
        /// Build a person in a product.
        /// </summary>
        public static PersonInProduct CreatePersonInProduct(int id, string name, Role role)
        {
            return new PersonInProduct
            {
                Id = id,
                Name = name,
                Role = role
            };
        }
    }
}