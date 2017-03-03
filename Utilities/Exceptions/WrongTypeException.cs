﻿using System;

namespace Utilities.Exceptions
{
    public class WrongTypeException : Exception
    {
        public WrongTypeException()
        {
        }

        public WrongTypeException(string message)
            : base(message)
        {
        }

        public WrongTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}