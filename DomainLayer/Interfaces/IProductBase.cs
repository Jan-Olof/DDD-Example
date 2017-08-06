namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product model base interface.
    /// </summary>
    public interface IProductBase<T> : IUpdateMapper<T>, IProductUpdate where T : IIdentifier, IName
    {
    }
}