namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product model base interface.
    /// </summary>
    public interface IProductBase<T> : IUpdateMapper<T>, IProductBaseDto where T : IIdentifier, IName
    {
    }
}