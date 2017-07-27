namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product model base interface.
    /// </summary>
    public interface IProductBase<T> : IFunctions<T>, IProductBaseDto where T : IIdentifier, IName

    {
    }
}