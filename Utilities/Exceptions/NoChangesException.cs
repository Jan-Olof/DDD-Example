using System;

namespace Utilities.Exceptions
{
    public class NoChangesException : Exception
    {
        public NoChangesException()
        {
        }

        public NoChangesException(string message) : base(message)
        {
        }

        public NoChangesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}