using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
using InfrastructureLayer.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureLayer.Factories
{
    /// <summary>
    /// Builds persons.
    /// </summary>
    public static class PersonFactory
    {
        /// <summary>
        /// Build a Person from a PersonDao.
        /// </summary>
        public static Person CreatePerson(PersonDao productDao)
            => new Person
            {
                Id = productDao.Id,
                FirstName = productDao.FirstName,
                LastName = productDao.LastName,
                Products = CreateProductsInPerson(productDao.ProductPersons)
            };

        /// <summary>
        /// Build a PersonDao from a Person.
        /// </summary>
        public static PersonDao CreatePersonDao(Person person)
            => new PersonDao
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                ProductPersons = CreateProductPersons(person.Products, person.Id)
            };

        /// <summary>
        /// Build a PersonDto from a Person.
        /// </summary>
        public static PersonDto CreatePersonDto(Person person)
            => new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Name = person.Name,
                Products = person.Products
            };

        private static List<ProductPerson> CreateProductPersons(IEnumerable<ProductInPerson> personProducts, int personId)
            => personProducts
                .Select(p => ProductPersonFactory.CreateProductPerson(p, personId))
                .ToList();

        private static IList<ProductInPerson> CreateProductsInPerson(IEnumerable<ProductPerson> productPersons)
            => productPersons
                .Select(ProductPersonFactory.CreateProductInPerson)
                .ToList();
    }
}