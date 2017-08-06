namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person model functions interface.
    /// </summary>
    public interface IPersonBase<T> : IUpdateMapper<T>, IPersonCreate, IIdentifier, IName where T : IIdentifier, IName
    {
    }
}