namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product model base interface.
    /// </summary>
    public interface IProductBase<T> : IUpdateMapper<T>, IProductCreate, IIdentifier where T : IIdentifier, IName
    {
    }
}