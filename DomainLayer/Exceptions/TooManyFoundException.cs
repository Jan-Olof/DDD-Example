using System;

namespace DomainLayer.Exceptions
{
    public class TooManyFoundException : Exception
    {
        public TooManyFoundException()
        {
        }

        public TooManyFoundException(string message)
            : base(message)
        {
        }

        public TooManyFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}