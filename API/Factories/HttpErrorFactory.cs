using System;
using API.Models;

namespace API.Factories
{
    /// <summary>
    /// Class that builds HttpError objects.
    /// </summary>
    public class HttpErrorFactory
    {
        /// <summary>
        /// Build an HttpError object.
        /// </summary>
        public static HttpError CreateHttpError(Exception exception)
        {
            return new HttpError
            {
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().FullName,
                StackTrace = exception.StackTrace,
                HResult = exception.HResult
            };
        }
    }
}