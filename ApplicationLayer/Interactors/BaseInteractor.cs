﻿using System;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Infrastructure;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer.Interactors
{
    /// <summary>
    /// Base interactor class.
    /// </summary>
    public abstract class BaseInteractor
    {
        protected readonly ILogger Logger;
        protected readonly IDomainRepository Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseInteractor"/> class.
        /// </summary>
        protected BaseInteractor(ILogger logger, IDomainRepository repository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Throw TooManyFoundException if the object is not null.
        /// </summary>
        // ReSharper disable once UnusedParameter.Global
        protected static void CheckNotNull<T>(T entity)
        {
            if (entity != null)
            {
                throw new TooManyFoundException($"The {nameof(entity)} object is not null.");
            }
        }

        /// <summary>
        /// Throw ArgumentNullException if the object is null.
        /// </summary>
        protected static void CheckNull<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}