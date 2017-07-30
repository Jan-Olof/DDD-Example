using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.EventLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
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
            var productDao = ProductFactory.CreateProductDao(product);

            productDao = Insert(productDao);

            return ProductFactory.CreateProduct(productDao);
        }

        public Person GetPerson(int id)
        {
            var personDao = Get<PersonDao>(id);

            return PersonFactory.CreatePerson(personDao);
        }

        /// <summary>
        /// Get all persons.
        /// </summary>
        public IEnumerable<Person> GetPersons()
        {
            var personDaos = Get<PersonDao>()
                .Include(p => p.ProductPersons)
                .ThenInclude(p => p.Product);

            return personDaos.Select(PersonFactory.CreatePerson);
        }

        /// <summary>
        /// Get persons based on a condition.
        /// </summary>
        public IEnumerable<Person> GetPersons(string name, bool isSearch = false)
        {
            var personDaos = Get(isSearch ? PersonDao.Search(name) : PersonDao.Get(name))
                .Include(p => p.ProductPersons)
                .ThenInclude(p => p.Product);

            return personDaos.Select(PersonFactory.CreatePerson);
        }

        public Product GetProduct(int id)
        {
            var productDao = Get<ProductDao>(id);

            return ProductFactory.CreateProduct(productDao);
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public IEnumerable<Product> GetProducts()
        {
            var productDaos = Get<ProductDao>()
                .Include(p => p.ProductPersons)
                .ThenInclude(p => p.Person);

            return productDaos.Select(ProductFactory.CreateProduct);
        }

        /// <summary>
        /// Get products based on a condition.
        /// </summary>
        public IEnumerable<Product> GetProducts(string name, bool isSearch = false)
        {
            var productDaos = Get(isSearch ? ProductDao.Search(name) : ProductDao.Get(name))
                .Include(p => p.ProductPersons)
                .ThenInclude(p => p.Person);

            return productDaos.Select(ProductFactory.CreateProduct);
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        public void RemovePerson(int id)
        {
            Delete<PersonDao>(id);
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        public void RemoveProduct(int id)
        {
            Delete<ProductDao>(id);
        }

        /// <summary>
        /// Update a person.
        /// </summary>
        public void UpdatePerson(Person person)
        {
            var personDao = PersonFactory.CreatePersonDao(person);

            var daoFromDb = Update(personDao);

            daoFromDb.ProductPersons = UpdateProductPerson(daoFromDb.ProductPersons, personDao.ProductPersons);

            SaveChanges(personDao, EventType.Update);
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        public void UpdateProduct(Product product)
        {
            var productDao = ProductFactory.CreateProductDao(product);

            var daoFromDb = Update(productDao);

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