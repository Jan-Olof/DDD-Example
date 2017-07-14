namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person properties interface.
    /// </summary>
    public interface IPersonProps : IIdentifier, IName
    {
        /// <summary>
        /// The first name of the person.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// The last name of the person.
        /// </summary>
        string LastName { get; set; }
    }
}