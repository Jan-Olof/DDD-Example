using DomainLayer.Models;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The product interface.
    /// </summary>
    public interface IProduct : IProductBase<Product>, IProductDto
    {
    }
}