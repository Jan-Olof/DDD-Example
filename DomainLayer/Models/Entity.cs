using System;
using System.Linq.Expressions;
using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// The base class for the domain models.
    /// </summary>
    public abstract class Entity<T> where T : IIdentifier, IName
    {
        /// <summary>
        /// Gets or sets the id. The primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name. This is the name of the entity.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Defines how to get entities by id.
        /// </summary>
        public static Expression<Func<T, bool>> Get(int id)
            => entity => entity.Id == id;

        /// <summary>
        /// Defines how to get entities by name.
        /// </summary>
        public static Expression<Func<T, bool>> Get(string name)
            => entity => entity.Name == name;

        /// <summary>
        /// Defines how to search entities by name.
        /// </summary>
        public static Expression<Func<T, bool>> Search(string name)
            => entity => entity.Name.ToLower().Contains(name.ToLower());
    }
}