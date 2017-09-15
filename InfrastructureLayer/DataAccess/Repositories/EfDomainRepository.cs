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
    /// The entity framwork implementation of the domain repository.
    /// </summary>
    public class EfDomainRepository : EfRepositoryBase, ICommands, IQueries
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
            var productDao = ProductFactory.CreateProductDao(product);

            productDao = Insert(productDao);

            return ProductFactory.CreateProduct(productDao);
        }

        /// <summary>
        /// Get a person from Id.
        /// </summary>
        public Person GetPerson(int id)
        {
            var personDao = Get(id, PersonDao.IncludeMembers());

            return PersonFactory.CreatePerson(personDao);
        }

        /// <summary>
        /// Get all persons.
        /// </summary>
        public List<Person> GetPersons()
        {
            var personDaos = Get(PersonDao.IncludeMembers());

            return personDaos.AsEnumerable().Select(PersonFactory.CreatePerson).ToList();
        }

        /// <summary>
        /// Get persons based on a condition.
        /// </summary>
        public List<Person> GetPersons(string name, bool isSearch = false)
        {
            var personDaos = Get(isSearch ? Entity.Search<PersonDao>(name) : Entity.Get<PersonDao>(name), PersonDao.IncludeMembers());

            return personDaos
                .AsEnumerable()
                .Select(PersonFactory.CreatePerson)
                .ToList();
        }

        /// <summary>
        /// Get a product from Id.
        /// </summary>
        public Product GetProduct(int id)
        {
            var productDao = Get(id, ProductDao.IncludeMembers());

            return ProductFactory.CreateProduct(productDao);
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public List<Product> GetProducts()
        {
            var productDaos = Get(ProductDao.IncludeMembers());

            return productDaos
                .AsEnumerable()
                .Select(ProductFactory.CreateProduct)
                .ToList();
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public List<Product> GetProducts(string name, bool isSearch = false)
        {
            var productDaos = Get(isSearch ? Entity.Search<ProductDao>(name) : Entity.Get<ProductDao>(name), ProductDao.IncludeMembers());

            return productDaos.AsEnumerable().Select(ProductFactory.CreateProduct).ToList();
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