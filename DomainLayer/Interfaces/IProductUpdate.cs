﻿namespace DomainLayer.Interfaces
{
    /// <summary>
    /// The interface of what you need when you uådate a product.
    /// </summary>
    public interface IProductUpdate : IIdentifier, IName
    {
        /// <summary>
        /// Gets or sets the description. A text field that describes the product.
        /// </summary>
        string Description { get; set; }
    }
}