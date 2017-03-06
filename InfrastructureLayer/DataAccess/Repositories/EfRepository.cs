using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities;
using Utilities.Enums;
using Utilities.Exceptions;

namespace InfrastructureLayer.DataAccess.Repositories
{
    public class EfRepository<T, TModel> : IRepository<T>, IDisposable where T : class where TModel : class, T
    {
        private readonly DbContext _context;
        private readonly ILogger _logger;
        private readonly IUpdateMapper<T> _updateMapper;

        public EfRepository(DbContext dataContext, IUpdateMapper<T> updateMapper, ILogger<EfRepository<T, TModel>> logger)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            if (updateMapper == null)
            {
                throw new ArgumentNullException(nameof(updateMapper));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _context = dataContext;
            _updateMapper = updateMapper;
            _logger = logger;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<TModel>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> condition)
        {
            var expression = ChangeType.ChangeInputType<T, TModel, bool>(condition);

            return _context.Set<TModel>().Where(expression);
        }

        public T Insert(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Update(T entity, Expression<Func<T, bool>> condition)
        {
            try
            {
                var toUpdate = _context.Set<TModel>().SingleOrDefault(condition);

                if (toUpdate == null)
                {
                    throw new NullReferenceException("No value was found for toUpdate.");
                }

                _updateMapper.MapUpdate(entity, toUpdate);

                int changes = _context.SaveChanges();
                _logger.LogInformation($"Saved {changes} changes.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError((int)LoggingEvents.Error, e, e.Message);
                throw new TooManyFoundException(e.Message, e);
            }
        }
    }
}