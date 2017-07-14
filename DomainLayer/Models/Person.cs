using DomainLayer.Interfaces;

namespace DomainLayer.Models
{
    /// <summary>
    /// This is the person domain model.
    /// </summary>
    public sealed class Person : Entity<Person>, IPerson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        /// <summary>
        /// The first name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The name of the person. Gets first and last name.
        /// </summary>
        public override string Name { get => $"{FirstName} {LastName}"; set { } }

        /// <summary>
        /// Updates the fields that are supposed to be updated when editing a person.
        /// </summary>
        public Person MapUpdate(Person from, Person to)
        {
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;

            return to;
        }
    }
}