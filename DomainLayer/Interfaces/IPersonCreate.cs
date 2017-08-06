namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The interface of what you need when you create a person.
    /// </summary>
    public interface IPersonCreate
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        string LastName { get; set; }
    }
}