namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person model functions interface.
    /// </summary>
    public interface IPersonBase<T> : IUpdateMapper<T>, IPersonBaseDto where T : IIdentifier, IName
    {
    }
}