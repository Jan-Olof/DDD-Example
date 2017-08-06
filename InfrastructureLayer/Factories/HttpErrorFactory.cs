using System;
using InfrastructureLayer.Dtos;

namespace InfrastructureLayer.Factories
{
    /// <summary>
    /// Class that builds HttpError objects.
    /// </summary>
    public static class HttpErrorFactory
    {
        /// <summary>
        /// Build an HttpError object.
        /// </summary>
        public static HttpError CreateHttpError(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            return new HttpError(
                exception.Message,
                exception.GetType().FullName,
                exception.HResult,
                CreateHttpError(exception.InnerException),
                exception.StackTrace);
        }
    }
}