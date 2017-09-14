using DomainLayer.Exceptions;
using DomainLayer.Interfaces;
using System;
using System.Linq.Expressions;

namespace DomainLayer.Models
{
    /// <summary>
    /// The common class for the domain models.
    /// </summary>
    public static class Entity
    {
        /// <summary>
        /// Throw TooManyFoundException if the object is not null.
        /// </summary>
        public static void CheckNotNull<T>(T entity)
        {
            if (entity != null)
            {
                throw new TooManyFoundException($"The {nameof(entity)} object is not null.");
            }
        }

        /// <summary>
        /// Throw ArgumentNullException if the object is null.
        /// </summary>
        public static void CheckNull<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        /// <summary>
        /// Defines how to get entities by id.
        /// </summary>
        public static Expression<Func<T, bool>> Get<T>(int id) where T : IIdentifier
            => entity => entity.Id == id;

        /// <summary>
        /// Defines how to get entities by name.
        /// </summary>
        public static Expression<Func<T, bool>> Get<T>(string name) where T : IName
            => entity => entity.Name == name;

        /// <summary>
        /// Defines how to search entities by name.
        /// </summary>
        public static Expression<Func<T, bool>> Search<T>(string name) where T : IName
            => entity => entity.Name.ToLower().Contains(name.ToLower());
    }
}