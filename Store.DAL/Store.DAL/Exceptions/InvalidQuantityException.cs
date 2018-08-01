using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DAL.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() { }
        public InvalidQuantityException(string message) : base(message) { }
        public InvalidQuantityException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
