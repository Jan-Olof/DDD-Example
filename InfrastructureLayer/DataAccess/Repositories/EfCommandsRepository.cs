using ApplicationLayer.Infrastructure;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
using InfrastructureLayer.EventLogging;
using InfrastructureLayer.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The entity framwork implementation of the domain commands repository.
    /// </summary>
    public class EfCommandsRepository : EfRepositoryBase, ICommands
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfCommandsRepository"/> class.
        /// </summary>
        public EfCommandsRepository(DbContext dataContext, ILogger<EfCommandsRepository> logger)
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
            var productDao = ProductFactory.CreateProductDao(product);

            productDao = Insert(productDao);

            return ProductFactory.CreateProduct(productDao);
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        public void RemovePerson(int id)
            => Delete<PersonDao>(id);

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void RemoveProduct(int id)
            => Delete<ProductDao>(id);

        /// <summary>
        /// Update a person.
        /// </summary>
        public void UpdatePerson(Person person)
        {
            var personDao = PersonFactory.CreatePersonDao(person);

            var daoFromDb = Update(personDao, PersonDao.IncludeMembers());

            daoFromDb.ProductPersons = UpdateProductPerson(daoFromDb.ProductPersons, personDao.ProductPersons);

            SaveChanges(personDao, EventType.Update);
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void UpdateProduct(Product product)
        {
            var productDao = ProductFactory.CreateProductDao(product);

            var daoFromDb = Update(productDao, ProductDao.IncludeMembers());

            daoFromDb.ProductPersons = UpdateProductPerson(daoFromDb.ProductPersons, productDao.ProductPersons);

            SaveChanges(productDao, EventType.Update);
        }

        private static List<ProductPerson> UpdateProductPerson(List<ProductPerson> fromDb, List<ProductPerson> updated)
        {
            var notInUpdated = fromDb.Except(updated).ToList();
            var notInDb = updated.Except(fromDb).ToList();

            foreach (var remove in notInUpdated)
            {
                fromDb.Remove(remove);
            }

            fromDb.AddRange(notInDb);

            return fromDb;
        }
    }
}