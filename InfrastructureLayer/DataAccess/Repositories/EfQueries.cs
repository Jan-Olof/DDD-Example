using ApplicationLayer.Infrastructure;
using DomainLayer.Exceptions;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.DataAccess.Daos;
using InfrastructureLayer.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LoggerExtensions = Helpers.Extensions.LoggerExtensions;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// The query class using entity framework.
    /// </summary>
    public class EfQueries : IQueries
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// The logging interface.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfQueries"/> class.
        /// </summary>
        public EfQueries(DbContext dbContext, ILogger logger)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => _context?.Dispose();

        public Person GetPerson(int id, bool includeMembers = true)
        {
            var personDao = includeMembers ? Get(id, PersonDao.IncludeMembers()) : Get<PersonDao>(id);

            return PersonFactory.CreatePerson(personDao);
        }

        public List<Person> GetPersons(bool includeMembers = true)
        {
            var personDaos = includeMembers ? Get(PersonDao.IncludeMembers()) : Get<PersonDao>();

            return personDaos
                .AsParallel()
                .Select(PersonFactory.CreatePerson)
                .ToList();
        }

        public List<Person> GetPersons(string name, bool isSearch = false, bool includeMembers = true)
        {
            var function = isSearch ? Entity.Search<PersonDao>(name) : Entity.Get<PersonDao>(name);

            var personDaos = includeMembers ? Get(function, PersonDao.IncludeMembers()) : Get(function);

            return personDaos
                .AsParallel()
                .Select(PersonFactory.CreatePerson)
                .ToList();
        }

        public Product GetProduct(int id, bool includeMembers = true)
        {
            var productDao = includeMembers ? Get(id, ProductDao.IncludeMembers()) : Get<ProductDao>(id);
            return ProductFactory.CreateProduct(productDao);
        }

        public List<Product> GetProducts(bool includeMembers = true)
        {
            var productDaos = includeMembers ? Get(ProductDao.IncludeMembers()) : Get<ProductDao>();

            return productDaos
                .AsParallel()
                .Select(ProductFactory.CreateProduct)
                .ToList();
        }

        public List<Product> GetProducts(string name, bool isSearch = false, bool includeMembers = true)
        {
            var function = isSearch ? Entity.Search<ProductDao>(name) : Entity.Get<ProductDao>(name);

            var productDaos = includeMembers ? Get(function, ProductDao.IncludeMembers()) : Get(function);

            return productDaos
                .AsParallel()
                .Select(ProductFactory.CreateProduct)
                .ToList();
        }

        /// <summary>
        /// Find an entity and handle null exception.
        /// </summary>
        private T FindEntity<T>(int id, Func<IQueryable<T>, IQueryable<T>> includeMembers) where T : class, IIdentifier
        {
            try
            {
                var entity = includeMembers(_context.Set<T>().AsNoTracking()).SingleOrDefault(arg => arg.Id == id);

                if (entity == null)
                {
                    throw new NotFoundException($"Could not find an object with id {id}.");
                }

                return entity;
            }
            catch (InvalidOperationException ex)
            {
                LoggerExtensions.LogErrorWithInnerExceptions(_logger, ex);
                throw new TooManyFoundException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Find an entity and handle null exception.
        /// </summary>
        private T FindEntity<T>(int id) where T : class, IIdentifier
        {
            try
            {
                var entity = _context.Set<T>().AsNoTracking().SingleOrDefault(arg => arg.Id == id);

                if (entity == null)
                {
                    throw new NotFoundException($"Could not find an object with id {id}.");
                }

                return entity;
            }
            catch (InvalidOperationException ex)
            {
                LoggerExtensions.LogErrorWithInnerExceptions(_logger, ex);
                throw new TooManyFoundException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get entity from primary key.
        /// </summary>
        private T Get<T>(int id, Func<IQueryable<T>, IQueryable<T>> includeMembers) where T : class, IIdentifier
            => FindEntity(id, includeMembers);

        /// <summary>
        /// Get entity from primary key.
        /// </summary>
        private T Get<T>(int id) where T : class, IIdentifier
            => FindEntity<T>(id);

        /// <summary>
        /// Get all entities of a certain type.
        /// </summary>
        private IQueryable<T> Get<T>() where T : class
            => _context.Set<T>().AsNoTracking();

        /// <summary>
        /// Get all entities of a certain type.
        /// </summary>
        private IQueryable<T> Get<T>(Func<IQueryable<T>, IQueryable<T>> includeMembers) where T : class
            => includeMembers(_context.Set<T>().AsNoTracking());

        /// <summary>
        /// Get all entities of a certain type based on a condition.
        /// </summary>
        private IQueryable<T> Get<T>(Expression<Func<T, bool>> condition) where T : class
            => _context.Set<T>().AsNoTracking().Where(condition);

        /// <summary>
        /// Get all entities of a certain type based on a condition.
        /// </summary>
        private IQueryable<T> Get<T>(Expression<Func<T, bool>> condition, Func<IQueryable<T>, IQueryable<T>> includeMembers) where T : class
            => includeMembers(_context.Set<T>().AsNoTracking().Where(condition));
    }
}