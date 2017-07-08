namespace DomainLayer.Interfaces
{
    /// <summary>
    /// Interface for the primary key.
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Gets or sets the id. The primary key used in our models.
        /// </summary>
        int Id { get; set; }
    }
}