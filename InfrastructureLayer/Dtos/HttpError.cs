﻿// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace InfrastructureLayer.Dtos
{
    /// <summary>
    /// This class is used to deliver an exception as a DTO.
    /// </summary>
    public class HttpError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpError"/> class.
        /// </summary>
        public HttpError()
        {
            ExceptionMessage = string.Empty;
            ExceptionType = string.Empty;
            StackTrace = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpError"/> class.
        /// </summary>
        public HttpError(string exceptionMessage, string exceptionType, int hResult, HttpError innerError, string stackTrace)
        {
            ExceptionMessage = exceptionMessage;
            ExceptionType = exceptionType;
            HResult = hResult;
            InnerError = innerError;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        public string ExceptionMessage { get; }

        /// <summary>
        /// Gets the exception type.
        /// </summary>
        public string ExceptionType { get; }

        /// <summary>
        /// Gets the HResult.
        /// </summary>
        public int HResult { get; }

        /// <summary>
        /// Gets the inner error..
        /// </summary>
        public HttpError InnerError { get; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        public string StackTrace { get; }
    }
}