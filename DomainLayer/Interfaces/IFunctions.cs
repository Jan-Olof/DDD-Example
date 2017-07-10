﻿using System;
using System.Linq.Expressions;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// A generic interface for model functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFunctions<T> : IUpdateMapper<T> where T : IIdentifier
    {
        /// <summary>
        /// Defines how to get instructions by id.
        /// </summary>
        Expression<Func<T, bool>> Get(int id);
    }
}