using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationLayer.Interfaces.Models;
using ApplicationLayer.Interfaces.Infrastructure;

namespace InfrastructureLayer.DataAccess.Repositories
{
    /// <summary>
    /// A simple in memory repository for a certain entity.
    /// </summary>
    public class InMemoryRepository : IRepository<IInstruction>
    {
        // TODO: Add tests for Fill and Persist.

        private readonly IFileHandler<IList<IInstruction>> _fileHandler;
        private readonly IUpdateMapper<IInstruction> _updateMapper;
        private IList<IInstruction> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository{T}"/> class.
        /// </summary>
        public InMemoryRepository(IUpdateMapper<IInstruction> updateMapper, IFileHandler<IList<IInstruction>> fileHandler)
        {
            _entities = new List<IInstruction>();
            _updateMapper = updateMapper ?? throw new ArgumentNullException(nameof(updateMapper));
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository{T}"/> class.
        /// </summary>
        public InMemoryRepository(IUpdateMapper<IInstruction> updateMapper, IList<IInstruction> entities, IFileHandler<IList<IInstruction>> fileHandler)
        {
            _updateMapper = updateMapper ?? throw new ArgumentNullException(nameof(updateMapper));
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
        }

        /// <summary>
        /// Delete an entity object.
        /// </summary>
        public void Delete(IInstruction entity)
        {
            var item = _entities.SingleOrDefault(e => e.Id == entity.Id);

            int index = _entities.IndexOf(item);
            _entities.RemoveAt(index);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _entities = new List<IInstruction>();
        }

        /// <summary>
        /// Fill the data set with data from the data store.
        /// </summary>
        public void FillDataSet()
        {
            _entities = _fileHandler.Read();
        }

        /// <summary>
        /// Get all entity objects.
        /// </summary>
        public IEnumerable<IInstruction> Get()
        {
            return _entities;
        }

        /// <summary>
        /// Get entity objects based on a condition.
        /// </summary>
        public IEnumerable<IInstruction> Get(Expression<Func<IInstruction, bool>> condition)
        {
            return _entities.Where(condition.Compile());
        }

        /// <summary>
        /// Insert an entity object.
        /// </summary>
        public IInstruction Insert(IInstruction entity)
        {
            entity.Id = GetNextId();
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Persist data to the data store.
        /// </summary>
        public void PersistData()
        {
            _fileHandler.Write(_entities);
        }

        /// <summary>
        /// Update an entity object. This is based on a condition defining how to find the object.
        /// </summary>
        public void Update(IInstruction entity, Expression<Func<IInstruction, bool>> condition)
        {
            var toUpdate = _entities.SingleOrDefault(condition.Compile());

            _updateMapper.MapUpdate(entity, toUpdate);
        }

        /// <summary>
        /// Get the next (unused) id.
        /// </summary>
        private int GetNextId()
        {
            if (_entities?.Any() == false)
            {
                return 1;
            }

            return _entities.Max(n => n.Id) + 1;
        }
    }
}