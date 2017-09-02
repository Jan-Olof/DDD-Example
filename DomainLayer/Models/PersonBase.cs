using DomainLayer.Interfaces;
using System;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the person domain model.
    /// </summary>
    public abstract class PersonBase<T> : Entity<T>, IPersonBase<T> where T : IPersonBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonBase{T}"/> class.
        /// </summary>
        protected PersonBase()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the name of the person. Gets first and last name.
        /// </summary>
        public sealed override string Name
        {
            get => $"{FirstName} {LastName}"; set => SetName(value);
        }

        /// <summary>
        /// Updates the fields that are supposed to be updated when editing a person.
        /// </summary>
        public T MapUpdate(T from, T to)
        {
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;

            return to;
        }

        /// <summary>
        /// Sets first and last name.
        /// </summary>
        private void SetName(string value)
        {
            int index = value.IndexOf(" ", StringComparison.Ordinal);

            if (index <= 0 || index + 1 >= value.Length)
            {
                return;
            }

            FirstName = value.Substring(0, index);
            LastName = value.Substring(index + 1);
        }
    }
}