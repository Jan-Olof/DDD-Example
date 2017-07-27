namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The person model functions interface.
    /// </summary>
    public interface IPersonBase<T> : IFunctions<T>, IPersonBaseDto where T : IIdentifier, IName
    {
    }
}