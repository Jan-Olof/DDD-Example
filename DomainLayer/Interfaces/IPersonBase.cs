namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person model base interface.
    /// </summary>
    public interface IPersonBase<T> : IUpdateMapper<T>, IPersonUpdate, IName where T : IIdentifier, IName
    {
    }
}